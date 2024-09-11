const b = require("@babel/core");
const { getIconData, stringToIcon } = require("@iconify/utils");
const { locate } = require("@iconify/json");
const IconNotFoundError = require("../errors/IconNotFoundError");

const iconsAsObject = {};
const collectionsAsObject = {};

exports.isIconifyFile = isIconifyFile = (plugin) =>
  plugin.filename?.includes("Iconify") &&
  plugin.file.code.includes("@@iconify-code-gen");

exports.loadIcon = loadIcon = (iconName) => {
  const iconDetails = stringToIcon(iconName);

  if (!iconDetails) throw IconNotFoundError(iconName);

  const collectionPath = locate(iconDetails.prefix);

  const collectionExists = collectionsAsObject[iconDetails.prefix];

  if (!collectionExists) {
    collectionsAsObject[iconDetails.prefix] = collectionPath;
  }

  const collection = require(collectionsAsObject[iconDetails.prefix]);

  const icon = getIconData(collection, iconDetails.name);

  if (!icon) throw IconNotFoundError(iconName);

  iconsAsObject[iconName] = icon;

  return icon;
};

exports.loadIcons = loadIcons = (plugin) => {
  const { icons } = plugin.opts;

  if (!isIconifyFile(plugin)) return;

  Array.from(new Set(icons)).forEach(loadIcon);

  const ast = b.template.ast(
    `global.__ICONIFY__ICONS__ = ${JSON.stringify(iconsAsObject)}`
  );

  plugin.file.path.node.body.unshift(ast);
};

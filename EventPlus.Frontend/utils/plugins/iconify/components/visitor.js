const { locate } = require("@iconify/json");
const { stringToIcon, getIconData } = require("@iconify/utils");

module.exports = (babel) => ({
  JSXElement(path) {
    const { types: t } = babel;

    const { openingElement } = path.node;
    const tagName = openingElement.name.name;

    //if it is not Iconify - just skip
    if (tagName !== "Iconify") {
      return;
    }

    const iconAttribute = openingElement.attributes.find(
      (node) => t.isJSXAttribute(node) && node.name.name === "icon"
    );

    const iconName =
      iconAttribute?.value?.value ||
      iconAttribute?.value?.expression?.value ||
      iconAttribute?.value?.expression?.extra?.rawValue;

    //if no value - just skip
    if (!iconName) {
      return;
    }

    const icon = stringToIcon(iconName);

    const filename = locate(icon.prefix);

    let iconAsJson;

    try {
      iconAsJson = require(filename);
    } catch (error) {
      throw new Error(`Iconify: Could not find icon set "${icon.prefix}"`);
    }

    const iconData = getIconData(iconAsJson, icon.name);

    if (!iconData) {
      throw new Error(
        `Iconify: Icon not found!\nCould not find icon ${iconValue}\n\nCheck all icons at\nhttps://iconify.design/icon-sets/`
      );
    }
    
    const iconDataExpression = [
      t.objectProperty(t.stringLiteral("body"), t.stringLiteral(iconData.body)),
    ];
    
    if (iconData.width) {
      iconDataExpression.push(
        t.objectProperty(
          t.stringLiteral("width"),
          t.numericLiteral(iconData.width)
        )
      );
    }
    
    if (iconData.height) {
      iconDataExpression.push(
        t.objectProperty(
          t.stringLiteral("height"),
          t.numericLiteral(iconData.height)
        )
      );
    }
    
    const iconDataProp = t.jSXAttribute(
      t.jSXIdentifier("iconData"),
      t.jSXExpressionContainer(t.objectExpression(iconDataExpression))
    );
    
    openingElement.attributes.push(iconDataProp);
  },
});

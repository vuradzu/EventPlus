const visitor = require("./components/visitor.js");
const { loadIcons } = require("./components/pre.js");

module.exports = (babel) => {
  return {
    visitor: visitor(babel),
    pre() {
      loadIcons(this);
    },
  };
};

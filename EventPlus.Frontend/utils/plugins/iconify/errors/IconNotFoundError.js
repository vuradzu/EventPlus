module.exports = IconNotFoundError = (iconName) =>
  new Error(
    `[React Native Iconify]
    
    Icon "${iconName}" not found in babel.config.js
    
    You need to add it to the "icons" array in the plugin options and restart the bundler.
    
    Example:
    
    [
      "react-native-iconify",
      { icons: ["mdi:home"] }
    ]`
  );

module.exports = function (api) {
  api.cache(true);
  return {
    presets: ["babel-preset-expo"],
    plugins: [
      "nativewind/babel",
      [
        "./utils/plugins/iconify/index.js",
        {
          icons: ["mdi:heart", "mdi:google", "mdi:users-plus", "mdi:user", "fluent:tab-new-24-filled", "akar-icons:home", "akar-icons:person"],
        },
      ],
      "react-native-reanimated/plugin",
    ],
  };
};

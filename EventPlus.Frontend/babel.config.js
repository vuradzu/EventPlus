module.exports = function (api) {
  api.cache(true);
  return {
    presets: ["babel-preset-expo"],
    plugins: [
      "nativewind/babel",
      [
        "./utils/plugins/iconify/index.js",
        {
          icons: [
            "mdi:heart",
            "mdi:google",
            "mdi:users-plus",
            "mdi:user",
            "fluent:tab-new-24-filled",
            "akar-icons:home",
            "akar-icons:person",
            "octicon:sparkle-fill-16",
            "fluent:mic-sparkle-20-filled",
            "fluent:play-circle-sparkle-24-filled",
            "fluent:tab-desktop-multiple-sparkle-20-filled",
            "fluent:camera-sparkles-24-filled",
            "fluent:hexagon-sparkle-24-filled",
            "fluent:arrow-trending-sparkle-24-filled",
            "fluent:flash-sparkle-20-filled",
            "fluent:rectangle-landscape-sparkle-32-filled",
            "fluent:search-sparkle-28-filled",
            "fluent:paint-brush-sparkle-20-filled",
            "fluent:text-effects-sparkle-24-filled",
            "fluent:hat-graduation-sparkle-16-filled",
            "fluent:pen-sparkle-16-filled",
            "fluent:bot-sparkle-24-filled",
            "fa6-solid:hand-sparkles",
          ],
        },
      ],
      "react-native-reanimated/plugin",
    ],
  };
};

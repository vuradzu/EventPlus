/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./app/**/*.{js,jsx,ts,tsx}", "./components/**/*.{js,jsx,ts,tsx}"],
  theme: {
    extend: {
      colors: {
        text: {
          primary: {
            DEFAULT: "#FFFFFF",
            inverse: "#1E1E1E",
          },
          secondary: "#FFFFFFCC",
          tetriaty: "#FFFFFF80",
          system: {
            DEFAULT: "#007AFF",
            active: "#007AFF",
          },
        },
        input: {
          bg: "#FFFFFF0A",
          text: {
            primary: "#FFFFFF",
            secondary: "#FFFFFF80",
          },
        },
        button: {
          bg: {
            primary: "#7560F1",
            secondary: "#FFFFFF0A",
          },
          text: "#FFFFFF", //primary and secondary
        },
        icon: {
          primary: "#FFFFFF",
          secondary: "#FFFFFFCC",
          disabled: "#FFFFFF80",
          accent: "#7560F1",
          inverse: {
            DEFAULT: "#1E1E1E",
            subdued: "#1E1E1E5C",
          },
        },
        bg: {
          primary: {
            DEFAULT: "#1E1E1E",
            vr: "#111111",
          },
          surface: {
            1: {
              DEFAULT: "#FFFFFF0A",
              strong: "#272727"
            },
            2: "#FFFFFF80",
          },
          accent: "#7560F1",
          inverse: "#FFFFFF",
          splash: "#020108",
        },
        border: {
          primary: "#FFFFFF17",
          secondary: "#FFFFFF0A",
        },
        success: "#198754",
        info: "#007AFF",
        warning: "#FFC107",
        error: "#DC3545",
      },
      fontFamily: {
        sfregular: ["SF-Pro-Display-Regular"],
        sfmedium: ["SF-Pro-Display-Medium"],
        sfsemibold: ["SF-Pro-Display-Semibold"],
        sfbold: ["SF-Pro-Display-Bold"],
        sfheavy: ["SF-Pro-Display-Heavy"],
      },
    },
  },
  plugins: [],
};

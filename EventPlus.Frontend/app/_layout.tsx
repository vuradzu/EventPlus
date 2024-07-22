import { GoogleSignin } from "@react-native-google-signin/google-signin";
import { useFonts } from "expo-font";
import { SplashScreen, Stack } from "expo-router";
import { useEffect } from "react";
import Toast from "react-native-toast-message";
import { toastConfig } from "~/config/toastsConfig";
import { createEventModalOptions } from "./modals/create-event";

SplashScreen.preventAutoHideAsync();
GoogleSignin.configure({
  webClientId:
    "990916835718-2432dhl9ik00cqbs7pf111qp2nfgla3q.apps.googleusercontent.com",
});

const RootLayout = () => {
  const [fontsLoaded, error] = useFonts({
    "SF-Pro-Display-Regular": require("../assets/fonts/SF-Pro-Display-Regular.otf"),
    "SF-Pro-Display-Semibold": require("../assets/fonts/SF-Pro-Display-Semibold.otf"),
  });

  useEffect(() => {
    if (error) throw error;

    if (fontsLoaded) {
      SplashScreen.hideAsync();
    }
  }, [fontsLoaded, error]);

  if (!fontsLoaded && !error) return null;

  return (
    <>
      <Stack
        screenOptions={{
          headerShown: false,
        }}
      >
        <Stack.Screen name="index"></Stack.Screen>
        <Stack.Screen
          name="(auth)"
          options={{ animation: "none" }}
        ></Stack.Screen>

        <Stack.Screen
          name="modals/create-event"
          options={createEventModalOptions}
        ></Stack.Screen>
      </Stack>
      <Toast config={toastConfig} topOffset={60} />
    </>
  );
};

export default RootLayout;

import { GoogleSignin } from "@react-native-google-signin/google-signin";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { useFonts } from "expo-font";
import { SplashScreen, Stack } from "expo-router";
import { useEffect } from "react";
import Toast from "react-native-toast-message";
import { ReactQueryDevTools } from "~/components/ReactQueryDevTools/ReactQueryDevTools";
import { toastConfig } from "~/config/toastsConfig";
import { createCommandModalOptions } from "./modals/create-command/create-command";
import { createEventModalOptions } from "./modals/create-event";

SplashScreen.preventAutoHideAsync();
GoogleSignin.configure({
  webClientId:
    "990916835718-2432dhl9ik00cqbs7pf111qp2nfgla3q.apps.googleusercontent.com",
});
const queryClient = new QueryClient();

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
    <QueryClientProvider client={queryClient}>
      <Stack
        screenOptions={{
          headerShown: false,
        }}
      >
        <Stack.Screen name="index" />
        <Stack.Screen name="(auth)" options={{ animation: "none" }} />

        <Stack.Screen
          name="modals/create-event"
          options={createEventModalOptions}
        />
        <Stack.Screen
          name="modals/create-command/create-command"
          options={createCommandModalOptions}
        />
      </Stack>
      <Toast config={toastConfig} topOffset={60} />
      <ReactQueryDevTools />
    </QueryClientProvider>
  );
};

export default RootLayout;

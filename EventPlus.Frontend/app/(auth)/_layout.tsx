import { Stack } from "expo-router";

const AuthLayout = () => {
  return (
    <Stack screenOptions={{ headerShown: false }}>
      <Stack.Screen name="sign-in" />
      <Stack.Screen
        name="set-up-profile"
        options={{
          animation: 'default',
          headerShown: true,
          headerTitle: "Set up profile",
          headerBackTitle: "Back",
          headerStyle: {
            backgroundColor: "#111111",
          },
          headerTitleStyle: {
            color: "#FFFFFF",
          },
        }}
      />
    </Stack>
  );
};

export default AuthLayout;

import { Stack } from "expo-router";
import React from "react";

const CommandsLayout = () => {
  return <Stack>
     <Stack.Screen
        name="enter-invite/enter-invite"
        options={{
          headerShown: true,
          headerBackTitle: "Back",
          headerStyle: {
            backgroundColor: "#111111",
          },
          headerTitle: ''
          // headerTitleStyle: {
            // color: "#FFFFFF",
          // },
        }}
      />
  </Stack>;
};

export default CommandsLayout;

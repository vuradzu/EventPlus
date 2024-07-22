import { Stack } from "expo-router";
import React from "react";

const EventsLayout = () => {
  return (
    <Stack>
      <Stack.Screen name="create" options={{presentation: 'modal'}}></Stack.Screen>
    </Stack>
  );
};

export default EventsLayout;

import React from "react";
import { DevToolsBubble } from "react-native-react-query-devtools";

export const ReactQueryDevTools = () => {
  const isDevelopment = process.env.EXPO_PUBLIC_IS_DEVELOPMENT === 'true';
  return isDevelopment ? <DevToolsBubble /> : null;
};

import { StyleProp, TextStyle, ViewStyle } from "react-native";
import {
  BaseToast,
  ErrorToast,
  InfoToast,
  ToastConfig,
} from "react-native-toast-message";

const contentContainerStyle: StyleProp<ViewStyle> = {
  backgroundColor: "#111111",
  shadowColor: "#FFFFFF",
  shadowOffset: { width: 0.2, height: 0.2 },
  shadowOpacity:  0.2,
  shadowRadius: 7,
  elevation: 7,
  borderTopRightRadius: 5,
  borderBottomRightRadius: 5
};

const text1Style: StyleProp<TextStyle> = {
  color: "#FFFFFF",
}

const text2Style: StyleProp<TextStyle> = {
  color: "#FFFFFFCC",
}

export const toastConfig: ToastConfig = {
  success: (props) => (
    <BaseToast
      {...props}
      style={{ borderLeftColor: "#4BB543" }}
      contentContainerStyle={contentContainerStyle}
      text1Style={text1Style}
      text2Style={text2Style}
    />
  ),
  info: (props) => (
    <InfoToast
      {...props}
      style={{ borderLeftColor: "#007AFF" }}
      contentContainerStyle={contentContainerStyle}
      text1Style={text1Style}
      text2Style={text2Style}
    />
  ),
  warning: (props) => (
    <InfoToast
      {...props}
      style={{ borderLeftColor: "#ffc107" }}
      contentContainerStyle={contentContainerStyle}
      text1Style={text1Style}
      text2Style={text2Style}
    />
  ),
  error: (props) => (
    <ErrorToast
      {...props}
      style={{ borderLeftColor: "#dc3545" }}
      contentContainerStyle={contentContainerStyle}
      text1Style={text1Style}
      text2Style={text2Style}
    />
  ),
};

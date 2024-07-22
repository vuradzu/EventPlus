import Toast, { ToastShowParams } from "react-native-toast-message";

type ToastOptions = Omit<ToastShowParams, "type" | "text1" | "text2"> & {
  message?: string;
  title?: string;
};

export const useToasts = () => {
  const showSuccessToast = (options?: ToastOptions) => {
    Toast.show({
      ...options,
      type: "success",
      text1: options?.title ?? "Успіх!",
      text2: options?.message,
    });
  };

  const showInfoToast = (options?: ToastOptions) => {
    Toast.show({
      ...options,
      type: "info",
      text1: options?.title ?? "Інформація",
      text2: options?.message,
    });
  };

  const showWarningToast = (options?: ToastOptions) => {
    Toast.show({
      ...options,
      type: "warning",
      text1: options?.title ?? "Застереження",
      text2: options?.message,
    });
  };

  const showErrorToast = (options?: ToastOptions) => {
    Toast.show({
      ...options,
      type: "error",
      text1: options?.title ?? "Помилка :(",
      text2: options?.message,
    });
  };

  return {
    showSuccessToast,
    showInfoToast,
    showWarningToast,
    showErrorToast,
  };
};

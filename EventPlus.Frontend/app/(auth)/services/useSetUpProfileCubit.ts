import * as FileSystem from "expo-file-system";
import * as ImagePicker from "expo-image-picker";
import { router } from "expo-router";
import { useEffect, useMemo, useState } from "react";
import { AuthenticateRequest } from "~/api/jwt/types/authenticateRequest";
import { changeAvatar, checkIfUsernameAvailable } from "~/api/user/user.api";
import { emailRegex, nameRegex, usernameRegex } from "~/constants/Regex";
import { useUserStore } from "~/store/user/user.store";
import { FormErrors } from "~/types/FormErrors";
import { JwtHelper } from "~/utils/helpers/jwtHelper";
import { useForm } from "~/utils/hooks/useForm";
import { useToasts } from "~/utils/hooks/useToasts";
import { useAuthStore } from "../state/auth.store";

export const useSetUpProfileCubit = () => {
  const { showWarningToast } = useToasts();

  const { updateUserAvatar } = useUserStore();
  const { providerUser } = useAuthStore();
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [pickedImage, setPickedImage] = useState<
    ImagePicker.ImagePickerAsset | undefined
  >();

  const {
    form: authRequest,
    setForm: setAuthRequest,
    setFormValue: setAuthRequestValue,
    formErrors,
    setFormErrors,
  } = useForm<AuthenticateRequest>();

  const getFileInfo = async (fileURI: string) => {
    const fileInfo = await FileSystem.getInfoAsync(fileURI);
    return fileInfo;
  };

  const isLessThanTheMB = (fileSize: number, border: number) =>
    fileSize / 1024 / 1024 <= border;

  const pickImage = async () => {
    // No permissions request is necessary for launching the image library
    let result = await ImagePicker.launchImageLibraryAsync({
      mediaTypes: ImagePicker.MediaTypeOptions.Images,
      allowsEditing: true,
      aspect: [4, 3],
      quality: 1,
      allowsMultipleSelection: false,
    });

    if (result.canceled) return;

    const { uri } = result.assets[0];
    const fileInfo = await getFileInfo(uri);

    if (!fileInfo.exists) return;

    if (!fileInfo.size || !isLessThanTheMB(fileInfo.size, 5)) {
      showWarningToast({
        title: "Занадто великий файл",
        message: "Дозволено 5 Мб",
      });
      return;
    }

    setPickedImage(result.assets[0]);
  };

  const accountImage = useMemo(() => {
    return {
      uri:
        pickedImage?.uri ??
        authRequest.avatar ??
        require("~/assets/images/anonimousAvatar.png"),
    };
  }, [authRequest.avatar, pickedImage]);

  const validateForm = async () => {
    setFormErrors({});
    const tempFormErrors: FormErrors<AuthenticateRequest> = {};

    if (!authRequest.username) {
      tempFormErrors.username = "Юзернейм не може бути пустим";
    }

    const { isAvailable } = await checkIfUsernameAvailable(
      authRequest.username!
    );
    if (!isAvailable) {
      tempFormErrors.username = "Цей юзернейм вже зайнятий";
    }

    if (!usernameRegex.test(authRequest.username!))
      tempFormErrors.username = "Недопустимі символи";

    if (!authRequest.firstName || !nameRegex.test(authRequest.firstName))
      tempFormErrors.firstName = "Невірний формат";

    if (authRequest.lastName) {
      if (!nameRegex.test(authRequest.lastName))
        tempFormErrors.lastName = "Невірний формат";
    }

    if (!authRequest.email || !emailRegex.test(authRequest.email))
      tempFormErrors.email = "Невірний формат";

    setFormErrors(tempFormErrors);

    return Object.keys(tempFormErrors).length === 0;
  };

  const uploadUserImage = async () => {
    const imageType = !!pickedImage?.uri
      ? "file"
      : !!authRequest.avatar
      ? "url"
      : "anon";

    return await changeAvatar(
      imageType === "file"
        ? {
            uri: pickedImage!.uri,
            extension: pickedImage!.uri.split(".").pop()!,
          }
        : imageType === "url"
        ? authRequest.avatar!
        : undefined
    );
  };

  const onSubmit = async () => {
    setIsLoading(true);
    try {
      const isValid = await validateForm();

      if (!isValid) {
        setIsLoading(false);
        return;
      }

      // registration
      await JwtHelper.authenticateUser({
        ...authRequest,
        providerMetadata: {
          token: await providerUser!.getIdToken(),
        },
      });

      const newAvatar = await uploadUserImage();

      updateUserAvatar(newAvatar);

      router.replace("command-onboarding");
    } finally {
      setIsLoading(false);
    }
  };

  useEffect(() => {
    setAuthRequest(() => ({
      type: "register",
      avatar: providerUser?.photoURL,
      provider: "google",
      providerKey: providerUser?.uid,
    }));

    const splittedDisplayNameArray = providerUser?.displayName?.split(" ");

    setAuthRequestValue("firstName", splittedDisplayNameArray![0]);

    if (splittedDisplayNameArray?.length ?? 0 > 1) {
      setAuthRequestValue("lastName", splittedDisplayNameArray![1]);
    }

    setAuthRequestValue(
      "username",
      providerUser!.email!.split("@")[0]!.substring(0, 12)
    );
    setAuthRequestValue("email", providerUser!.email);
  }, []);

  return {
    isLoading,
    pickImage,
    onSubmit,
    form: authRequest,
    setFormValue: setAuthRequestValue,
    formErrors,
    accountImage,
  };
};

import { router } from "expo-router";
import { useState } from "react";
import { useInvite } from "~/api/command/command.api";
import { useUserStore } from "~/store/user/user.store";
import { JwtHelper } from "~/utils/helpers/jwtHelper";
import { useForm } from "~/utils/hooks/useForm";

export const useEnterInviteCubit = () => {
  const [isLoading, setIsLoading] = useState<boolean>(false);

  const {
    form: codeForm,
    setForm: setCodeForm,
    formErrors: codeErrors,
    setFormErrors: setCodeError,
  } = useForm<{ code: string }>();

  const { setActiveCommand } = useUserStore();

  const validateForm = () => {
    setCodeError({});
    let tempCodeError = "";

    if (!codeForm.code || codeForm.code.length < 5)
      tempCodeError = "Невірний код";

    setCodeError({ code: tempCodeError });

    return !tempCodeError;
  };

  const convertCodeResultError = (message: string) => {
    switch (message) {
      case "Invitation has already been expired":
        return "Термін дії запрошення вже минув";
      case "You already are a member of this group":
        return "Ви вже учасник даної команди";
      case "No such invitation":
        return "Такого запрошення не існує";
      default:
        return "";
    }
  };

  const onSubmit = async () => {
    setIsLoading(true);
    try {
      const isValid = validateForm();

      if (!isValid) {
        setIsLoading(false);
        return;
      }

      const inviteCodeResult = await useInvite(codeForm.code!);

      if (!inviteCodeResult.isSuccess) {
        setCodeError({
          code: convertCodeResultError(inviteCodeResult.message),
        });
        return;
      }

      setActiveCommand(inviteCodeResult.commandId);
      JwtHelper.addUserToken(inviteCodeResult.tokens, inviteCodeResult.commandId);

      router.replace("home/home");
    } finally {
      setIsLoading(false);
    }
  };

  return {
    onSubmit,
    codeForm,
    setCodeForm,
    codeErrors,
    isLoading,
  };
};

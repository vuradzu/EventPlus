import { useQueryClient } from "@tanstack/react-query";
import { router } from "expo-router";
import { useState } from "react";
import { _useInvite } from "~/api/command/command.api";
import { useCommandsStore } from "~/store/commands/commands.store";
import { JwtHelper } from "~/utils/helpers/jwtHelper";
import { QueryKeys } from "~/utils/helpers/queryKeys";
import { useForm } from "~/utils/hooks/useForm";

export const useEnterInviteCubit = () => {
  const queryClient = useQueryClient();

  const [isLoading, setIsLoading] = useState<boolean>(false);

  const {
    form: codeForm,
    setForm: setCodeForm,
    formErrors: codeErrors,
    setFormErrors: setCodeError,
  } = useForm<{ code: string }>();

  const { addCommand, setActiveCommand } = useCommandsStore();

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
      case "No such command":
        return "Група, в яку ви хочете доєднатись, більше не існує";
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

      const inviteCodeResult = await _useInvite(codeForm.code!);

      if (!inviteCodeResult.isSuccess) {
        setCodeError({
          code: convertCodeResultError(inviteCodeResult.message),
        });
        return;
      }

      setActiveCommand(inviteCodeResult.command.id);
      addCommand(inviteCodeResult.command);
      JwtHelper.addUserToken(
        inviteCodeResult.tokens,
        inviteCodeResult.command.id
      );
      queryClient.refetchQueries({ queryKey: QueryKeys.UserCommands });

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

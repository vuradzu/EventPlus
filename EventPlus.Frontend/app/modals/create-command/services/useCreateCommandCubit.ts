import { useQueryClient } from "@tanstack/react-query";
import { router } from "expo-router";
import { useState } from "react";
import { _createCommand } from "~/api/command/command.api";
import { useCommandsStore } from "~/store/commands/commands.store";
import { FormErrors } from "~/types/FormErrors";
import { JwtHelper } from "~/utils/helpers/jwtHelper";
import { QueryKeys } from "~/utils/helpers/queryKeys";
import { useForm } from "~/utils/hooks/useForm";

export const useCreateCommandCubit = (closeAllModals: boolean) => {
  const queryClient = useQueryClient();
  const { addCommand, setActiveCommand } = useCommandsStore();

  const [isLoading, setIsLoading] = useState<boolean>(false);

  const { form, setForm, formErrors, setFormErrors } = useForm<{
    name: string;
    description?: string;
  }>({
    name: "",
  });

  const validateForm = async () => {
    setFormErrors({});
    const tempFormErrors: FormErrors<{ name: string }> = {};

    if (!form.name) tempFormErrors.name = "Введіть назву";

    setFormErrors(tempFormErrors);

    return Object.keys(tempFormErrors).length === 0;
  };

  const onSubmit = async () => {
    setIsLoading(true);
    try {
      const isValid = await validateForm();

      if (!isValid) {
        setIsLoading(false);
        return;
      }

      const newCommand = await _createCommand(form.name!, form.description);

      addCommand(newCommand);
      setActiveCommand(newCommand.id);
      JwtHelper.addUserToken(newCommand.tokens, newCommand.id);
      queryClient.refetchQueries({ queryKey: QueryKeys.UserCommands });

      if (closeAllModals) router.dismissAll();
      else {
        router.back();
        router.replace("home/home");
      }
    } finally {
      setIsLoading(false);
    }
  };

  return {
    form,
    setForm,
    formErrors,
    onSubmit,
    isLoading,
  };
};

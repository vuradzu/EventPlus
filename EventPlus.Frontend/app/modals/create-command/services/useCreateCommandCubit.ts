import { router } from "expo-router";
import { useState } from "react";
import { createCommand } from "~/api/command/command.api";
import { useUserStore } from "~/store/user/user.store";
import { FormErrors } from "~/types/FormErrors";
import { JwtHelper } from "~/utils/helpers/jwtHelper";
import { useForm } from "~/utils/hooks/useForm";

export const useCreateCommandCubit = () => {
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const { setActiveCommand } = useUserStore();
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

      const newCommand = await createCommand(form.name!, form.description);
      setActiveCommand(newCommand.id);
      JwtHelper.addUserToken(newCommand.tokens, newCommand.id);

      router.replace("home/home");
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

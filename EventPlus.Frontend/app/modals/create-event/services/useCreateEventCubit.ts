import dayjs from "dayjs";
import { useState } from "react";
import { createEvent } from "~/api/event/event.api";
import { CreateEventRequest } from "~/api/event/types/createEventRequest";
import { useCommandsStore } from "~/store/commands/commands.store";
import { FormErrors } from "~/types/FormErrors";
import { useForm } from "~/utils/hooks/useForm";

export const useCreateEventCubit = () => {
  const { addCommandEvent } = useCommandsStore();

  const [isLoading, setIsLoading] = useState(false);
  const { form, setFormValue, formErrors, setFormErrors: setEventFormErrors } =
    useForm<CreateEventRequest>({
      priority: "medium",
    });

  const validateForm = () => {
    setEventFormErrors({});
    const tempFormErrors: FormErrors<CreateEventRequest> = {};

    if (!form.title) tempFormErrors.title = "Введіть назву";

    if (!form.date) tempFormErrors.date = "Введіть дату";
    if (dayjs().isAfter(form.date)) tempFormErrors.date = "Невірна дата";

    setEventFormErrors(tempFormErrors);

    return Object.keys(tempFormErrors).length === 0;
  };

  const onSubmit = async () => {
    setIsLoading(true);
    try {
      const isValid = validateForm();
      if (!isValid) return;

      const newEvent = await createEvent(form as CreateEventRequest);
      addCommandEvent(newEvent);
    } finally {
      setIsLoading(false);
    }
  };

  return {
    form,
    setFormValue,
    formErrors,
    isLoading,
    onSubmit,
  };
};

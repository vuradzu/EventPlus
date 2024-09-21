import { useQueryClient } from "@tanstack/react-query";
import dayjs from "dayjs";
import { router } from "expo-router";
import { useState } from "react";
import { createEvent } from "~/api/event/event.api";
import { CreateEventRequest } from "~/api/event/types/createEventRequest";
import { EventModelMini } from "~/api/event/types/eventModel";
import { useCommandsStore } from "~/store/commands/commands.store";
import { FormErrors } from "~/types/FormErrors";
import { QueryKeys } from "~/utils/helpers/queryKeys";
import { useForm } from "~/utils/hooks/useForm";

export const useCreateEventCubit = () => {
  const queryClient = useQueryClient();

  const { activeCommand } = useCommandsStore();
  const [isLoading, setIsLoading] = useState(false);
  const {
    form,
    setFormValue,
    formErrors,
    setFormErrors: setEventFormErrors,
  } = useForm<CreateEventRequest>({
    icon: "octicon:sparkle-fill-16",
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

      queryClient.setQueryData(
        QueryKeys.EventsByCommand(activeCommand!),
        (oldData: EventModelMini[]) => {
          return [newEvent, ...oldData];
        }
      );

      router.back();
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

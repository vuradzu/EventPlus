import { useState } from "react";
import { FormErrors } from "~/types/FormErrors";

export const useForm = <T>(defaultValue?: Partial<T>) => {
  const [form, setForm] = useState<Partial<T>>({ ...defaultValue });
  const [formErrors, setFormErrors] = useState<FormErrors<T>>({});

  const setFormValue = <K extends keyof T>(field: K, value: T[K]) => {
    setForm((prev) => ({
      ...prev,
      [field]: value,
    }));
  };

  const setError = (field: keyof typeof formErrors, message?: string) => {
    setFormErrors((prev) => ({
      ...prev,
      [field]: message ?? true,
    }));
  };

  return {
    form,
    setForm,
    setFormValue,
    formErrors,
    setFormErrors,
    setError,
  };
};

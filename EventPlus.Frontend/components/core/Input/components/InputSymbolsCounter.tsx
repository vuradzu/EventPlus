import React from "react";
import { classNames } from "~/utils/helpers/classNames";
import { Typography } from "../../Typography/Typography";

interface InputSymbolsCounterProps {
  maxLength?: number;
  value?: string | null;
}

export const InputSymbolsCounter = ({
  maxLength,
  value,
}: InputSymbolsCounterProps) => {
  return !!maxLength ? (
    <Typography
      className={classNames(
        "absolute",
        {
          "color-input-text-secondary": value?.length !== maxLength,
          "color-error": value?.length === maxLength,
        },
        ["absolute", "right-4", "bottom-2"]
      )}
      fontSize={10}
    >
      {value?.length ?? 0}/{maxLength}
    </Typography>
  ) : null;
};

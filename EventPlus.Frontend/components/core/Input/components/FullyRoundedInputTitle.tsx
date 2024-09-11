import React from "react";
import { classNames } from "~/utils/helpers/classNames";
import { Typography } from "../../Typography/Typography";
import { InputVariant } from "../types/InputVariant";

interface FullyRoundedInputTitleProps {
  variant: InputVariant;
  placeholder: string;
}

export const FullyRoundedInputTitle = ({
  variant,
  placeholder,
}: FullyRoundedInputTitleProps) => {
  return variant === InputVariant.FullyRounded ? (
    <Typography
      variant="b2"
      className={classNames("color-input-text-secondary", {}, ["mx-4"])}
    >
      {placeholder}
    </Typography>
  ) : null;
};

export default FullyRoundedInputTitle;

import { TextStyle } from "react-native";
import { TypographyVariants } from "./TypographyVariants";

type TypographyVariantsType = Record<TypographyVariants, string>;

export type FontStyle = Pick<
  TextStyle,
  "fontFamily" | "fontSize" | "fontWeight" | "letterSpacing"
>;

type VariantsType = { [P in keyof TypographyVariantsType]: FontStyle };

export const TypographyVariantsObject: VariantsType = {
  [TypographyVariants.Regular]: {
    fontSize: 17,
    fontWeight: "regular",
    letterSpacing: -0.41,
  },
  [TypographyVariants.Semibold]: {
    fontSize: 17,
    fontWeight: "semibold",
    letterSpacing: -0.41,
  },
};

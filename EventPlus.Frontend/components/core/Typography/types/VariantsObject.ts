import { TextStyle } from "react-native";
import { TypographyVariants } from "./TypographyVariants";

type TypographyTypesRecord = Record<TypographyVariants, string>;

export type FontWeight = "regular" | 'medium' | "semibold" | "bold" | "heavy";

export type FontStyle = Pick<
  TextStyle,
  "fontFamily" | "fontSize" | "letterSpacing"
> & { fontWeight: FontWeight };

type VariantsType = { [P in keyof TypographyTypesRecord]: FontStyle };

export const TypographyVariantsObject: VariantsType = {
  h1: { fontSize: 36, fontWeight: "heavy", letterSpacing: -0.41 },
  h2: { fontSize: 32, fontWeight: "bold", letterSpacing: -0.41 },
  h3: { fontSize: 28, fontWeight: "semibold", letterSpacing: -0.41 },
  h4: { fontSize: 24, fontWeight: "semibold", letterSpacing: -0.41 },
  h5: { fontSize: 20, fontWeight: "semibold", letterSpacing: -0.41 },
  b1: { fontSize: 17, fontWeight: "regular", letterSpacing: -0.41 },
  b2: { fontSize: 14, fontWeight: "regular", letterSpacing: -0.41 },
  b3: { fontSize: 12, fontWeight: "regular", letterSpacing: -0.41 },
  l: { fontSize: 11, fontWeight: "regular", letterSpacing: -0.41 },
};

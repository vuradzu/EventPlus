import { router } from "expo-router";
import { Button, ButtonVariants } from "~/components/core/Button/Button";

export const baseModalScreenOptions: any = {
  presentation: "modal",
  headerShown: true,
  headerBackButtonMenuEnabled: true,
  headerLeft: () => (
    <Button textStyles="color-text-system" variant={ButtonVariants.Transparent} onPress={() => router.back()}>
      Скасувати
    </Button>
  ),
};

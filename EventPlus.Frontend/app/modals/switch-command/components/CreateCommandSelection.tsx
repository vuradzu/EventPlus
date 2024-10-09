import { router } from "expo-router";
import { TouchableOpacity, View } from "react-native";
import { Iconify } from "~/components/core/Iconify/Iconify";
import { Typography } from "~/components/core/Typography/Typography";

export const CreateCommandSelection = () => {
  return (
    <View className="flex flex-col my-2">
      <View className="border-b-[1px] mb-3 border-[#FFFFFF17]" />
      <TouchableOpacity
        className="flex flex-row items-center"
        onPress={() =>
          router.push({
            pathname: "modals/create-command/create-command",
            params: { closeAllModals: "true" },
          })
        }
      >
        <View className="w-[48px] h-[48px] rounded-[8px] flex justify-center items-center bg-text-primary">
          <Iconify icon="flowbite:plus-outline" color="#7560F1" />
        </View>
        <View className="flex flex-column ml-3">
          <Typography fontWeight="bold">Створити нову команду</Typography>
        </View>
      </TouchableOpacity>
    </View>
  );
};

import { router } from "expo-router";
import React, { useMemo } from "react";
import { Dimensions, View } from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";
import { Button } from "~/components/core/Button/Button";
import { TypographyVariants } from "~/components/core/Typography/types/TypographyVariants";
import { Typography } from "~/components/core/Typography/Typography";
import { EventTilesDashboard } from "~/components/EventTiles/EventTilesDashboard";
import { useCommandsStore } from "~/store/commands/commands.store";
import { HomeFilter } from "./components/HomeFilter";
import { useHomeCubit } from "./services/useHomeCubit";

const Home = () => {
  const { activeCommand, commands } = useCommandsStore();
  const { eventsAccessor } = useHomeCubit();

  const { height: windowHeight } = Dimensions.get("window");

  const commandName = useMemo(
    () =>
      commands.find((c) => c.id === activeCommand)?.name ??
      "Немає активної команди",
    [activeCommand]
  );

  return (
    <SafeAreaView className="bg-bg-primary w-full h-full flex flex-col justify-between pb-[105]">
      <View className="h-full flex flex-col">
        <View className="flex flex-col py-5 px-4 border-b border-border-primary">
          <Typography variant={TypographyVariants.Semibold} fontSize={34}>
            {commandName}
          </Typography>
        </View>
        <View>
          <HomeFilter />
          {eventsAccessor.isLoading ? (
            <Typography>"Завантаження..."</Typography>
          ) : !eventsAccessor.isSuccess ? (
            <Typography>"Помилка завантаження"</Typography>
          ) : (
            <EventTilesDashboard events={eventsAccessor.data} />
          )}
        </View>
      </View>
      {/* button */}
      <View className="absolute right-4 bottom-5 flex items-end w-full">
        <Button
          icon={require("~/assets/icons/new_item.png")}
          onPress={() => router.push("/modals/create-event/create-event")}
          styles="w-[40%]"
          fontSize={17}
        >
          Нова подія
        </Button>
      </View>
    </SafeAreaView>
  );
};

export default Home;

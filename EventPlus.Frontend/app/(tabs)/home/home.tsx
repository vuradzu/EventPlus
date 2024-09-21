import { router } from "expo-router";
import React from "react";
import { View } from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";
import { CommandHeader } from "~/components/CommandHeader/CommandHeader";
import { Button, ButtonVariants } from "~/components/core/Button/Button";
import { EventTilesDashboard } from "~/components/EventTiles/EventTilesDashboard";
import { EventsFilter } from "./components/HomeFilter";
import { useHomeCubit } from "./services/useHomeCubit";

const Home = () => {
  const { eventsAccessor } = useHomeCubit();

  return (
    <SafeAreaView className="bg-bg-primary w-full h-full flex flex-col justify-between pb-[105]">
      <View className="h-full flex flex-col">
        <CommandHeader />
        <View>
          <EventsFilter />
          <EventTilesDashboard eventsAccessor={eventsAccessor} />
        </View>
      </View>
      {/* button */}
      <View className="absolute right-4 bottom-5 flex items-end w-full">
        <Button
          iconProps={{
            icon: "fluent:tab-new-24-filled",
            color: "white",
          }}
          onPress={() => router.push("/modals/create-event/create-event")}
          styles="w-[40%]"
          fontSize={17}
          variant={ButtonVariants.PrimaryBold}
        >
          Нова подія
        </Button>
      </View>
    </SafeAreaView>
  );
};

export default Home;

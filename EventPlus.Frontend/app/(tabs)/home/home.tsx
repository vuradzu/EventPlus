import { router } from "expo-router";
import React, { useState } from "react";
import { SafeAreaView, View } from "react-native";
import { DateType } from "react-native-ui-datepicker";
import { Button } from "~/components/core/Button/Button";
import { Calendar } from "~/components/core/Calendar/Calendar";
import { TypographyVariants } from "~/components/core/Typography/types/TypographyVariants";
import { Typography } from "~/components/core/Typography/Typography";
import { EventModel } from "~/components/EventTiles/EventTilesDashboard";
import { useCommandsStore } from "~/store/commands/commands.store";

const Home = () => {
  const events: EventModel[] = [
    {
      id: 1,
      title: "Test 1",
      priority: "low",
      date: new Date(),
    },
    {
      id: 2,
      title: "Test 2",
      priority: "medium",
      date: new Date(),
    },
    {
      id: 3,
      title: "Test 3",
      priority: "high",
      date: new Date(),
    },
    {
      id: 4,
      title: "Test 4",
      priority: "low",
      date: new Date(),
    },
  ];
  const [date, setDate] = useState<DateType>();

  const { activeCommand, commands } = useCommandsStore();

  const test =
    commands.find((c) => c.id === activeCommand)?.name ?? "No command";

  return (
    <SafeAreaView className="bg-bg-primary w-fsull h-full flex flex-col justify-between">
      {/* body */}
      <View>
        <View className="flex flex-col py-5 px-4 border-b border-border-primary">
          <Typography variant={TypographyVariants.Semibold} fontSize={34}>
            {test}
          </Typography>
        </View>
        <View className="bg-bg-primary-vr">
          {/* <HomeFilter />
          <EventTilesDashboard events={events} /> */}
          <Calendar date={date} setDate={setDate} />
        </View>
      </View>

      {/* button */}
      <View className="flex items-end w-full">
        <Button
          icon={require("~/assets/icons/new_item.png")}
          onPress={() => router.push("/modals/create-event/create-event")}
          styles="w-[40%] m-4"
          fontSize={17}
        >
          Нова подія
        </Button>
      </View>
    </SafeAreaView>
  );
};

export default Home;

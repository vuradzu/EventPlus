import { router } from "expo-router";
import React from "react";
import { SafeAreaView, View } from "react-native";
import { Button } from "~/components/core/Button/Button";
import { TypographyVariants } from "~/components/core/Typography/types/TypographyVariants";
import { Typography } from "~/components/core/Typography/Typography";
import {
  EventModel,
  EventTilesDashboard,
} from "~/components/EventTiles/EventTilesDashboard";
import { HomeFilter } from "./components/HomeFilter";

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

  return (
    <SafeAreaView className="bg-bg-primary w-fsull h-full flex flex-col justify-between">
      {/* body */}
      <View>
        <View className="flex flex-col py-5 px-4 border-b border-border-primary">
          <Typography variant={TypographyVariants.Semibold} fontSize={34}>
            Events
          </Typography>
        </View>
        <View>
          <HomeFilter />
          <EventTilesDashboard events={events} />
        </View>
      </View>

      {/* button */}
      <View className="flex items-end w-full">
        <Button
          icon={require("~/assets/icons/new_item.png")}
          onPress={() => router.push("/modals/create-event")}
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

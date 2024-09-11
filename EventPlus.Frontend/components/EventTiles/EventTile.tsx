import { Skeleton } from "moti/skeleton";
import React, { useMemo } from "react";
import { Dimensions, View } from "react-native";
import { EventModelMini } from "~/api/event/types/eventModel";
import { Typography } from "~/components/core/Typography/Typography";
import { LineProgress } from "../core/LineProgress/LineProgress";

interface EventTileProps {
  event: EventModelMini;
}

export const EventTile = ({ event }: EventTileProps) => {
  const { title, completedAssignmentsCount, assignmentsCount } = event;

  const progress = useMemo(
    () => completedAssignmentsCount / assignmentsCount,
    [completedAssignmentsCount, assignmentsCount]
  );

  const windowWidth = Dimensions.get("window").width;

  const tileDimension = useMemo(() => {
    const space = windowWidth * 0.2;
    const widthForTiles = windowWidth - space;

    return widthForTiles / 3;
  }, [event, windowWidth]);

  return (
    <View
      className="flex items-center"
      style={{ width: tileDimension, height: tileDimension }}
    >
      <View
        className="bg-bg-inverse rounded-2xl"
        style={{ width: tileDimension, height: tileDimension }}
      >
        {assignmentsCount >= 1 ? (
          <LineProgress
            progress={progress}
            unfilledStyles="w-[80%] absolute left-[10%] bottom-3"
          />
        ) : null}
      </View>
      <Typography variant="b2" fontWeight="bold" className="color-text-primary mt-1">{title}</Typography>
    </View>
  );
};

export const EventTileSkeleton = () => {
  const windowWidth = Dimensions.get("window").width;

  const tileDimension = useMemo(() => {
    const space = windowWidth * 0.2;
    const widthForTiles = windowWidth - space;

    return widthForTiles / 3;
  }, [windowWidth]);

  return (
    <View className="flex items-center">
      <View className="mb-3">
        <Skeleton width={tileDimension} height={tileDimension}/>
      </View>
        <Skeleton width={tileDimension - (tileDimension * 0.15)} height={14}/>
    </View>
  );
};

import React from "react";
import { Priority } from "~/api/types/Priority";
import { GridView } from "~/components/GridView/GridView";
import { EventTile } from "./EventTile";

export interface EventModel {
  id: number;

  title: string;
  description?: string | null;

  // creatorId: number;
  // commandId: number;

  priority: Priority;
  date: Date;

  // created: Date;
}

interface EventTilesDashboardProps {
  events: EventModel[];
}

export const EventTilesDashboard = (props: EventTilesDashboardProps) => {
  const { events } = props;

  return (
    <GridView
      items={events}
      renderItem={(event) => <EventTile event={event} />}
    />
  );
};

import React from "react";
import { Tab, Tabs } from "../Tabs/Tabs";

type Priorities = "low" | "medium" | "high";

interface PriorityTabsProps {
  priority?: Priorities;
  onPriorityChange?: (id: string) => void;
}

const PriorityTabs = (props: PriorityTabsProps) => {
  const { priority, onPriorityChange } = props;

  const priorityTabs: Tab[] = [
    { id: "low", title: "Низький" },
    { id: "medium", title: "Середній" },
    { id: "high", title: "Високий" },
  ];

  return (
    <Tabs
      tabs={priorityTabs}
      selectedTab={priority}
      onTabChange={onPriorityChange}
    />
  );
};

export default PriorityTabs;

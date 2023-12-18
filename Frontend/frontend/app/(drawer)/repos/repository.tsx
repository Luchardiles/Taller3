import { Drawer } from "expo-router/drawer";
import ReposScreen from "../../../components/repos/ReposScreen";
import { DrawerToggleButton } from "@react-navigation/drawer";

const Repos = () => {
  return (
    <>
      <Drawer.Screen
        options={{
          headerShown: true,
          title: "Repositorios",
          headerLeft: () => <DrawerToggleButton />,
        }}
      />
      <ReposScreen />
    </>
  );
};

export default Repos;

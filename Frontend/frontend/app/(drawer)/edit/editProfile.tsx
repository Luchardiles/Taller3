import { Drawer } from "expo-router/drawer";
import ReposScreen from "../../../components/repos/ReposScreen";
import { Text } from "react-native-paper";
import { DrawerToggleButton } from "@react-navigation/drawer";
import EditScreen from "../../../components/edit/EditScreen";

const Edit = () => {
  return (
    <>
      <Drawer.Screen
        options={{
          headerShown: true,
          title: "Editar perfil",
          headerLeft: () => <DrawerToggleButton />,
        }}
      />
      <EditScreen />
    </>
  );
};

export default Edit;

using Architecture;

namespace Scenes
{
    public sealed class GameSceneManager : SceneManagerBase
    {
        public override void InitScenesMap()
        {
            this.sceneConfigMap[MainMenuSceneConfig.SCENE_NAME] = new MainMenuSceneConfig();
            this.sceneConfigMap[SurvivalSceneConfig.SCENE_NAME] = new SurvivalSceneConfig();
            this.sceneConfigMap[ShopSceneConfig.SCENE_NAME] = new ShopSceneConfig();
            //инициализируем крохотный список сцен)))
        }
    }
}

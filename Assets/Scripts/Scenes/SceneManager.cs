namespace Architecture.Scenes
{
    public sealed class SceneManager : SceneManagerBase
    {
        public override void InitScenesMap()
        {
            this.sceneConfigMap[MainMenuSceneConfig.SCENE_NAME] = new MainMenuSceneConfig();
            this.sceneConfigMap[SurvivalSceneConfig.SCENE_NAME] = new SurvivalSceneConfig();
            //инициализируем крохотный список сцен)))
        }
    }
}

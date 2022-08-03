using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Scenes
{
    public sealed class SceneManagerExample : SceneManagerBase
    {
        public override void InitScenesMap()
        {
            this.sceneConfigMap[SceneConfigExample.SCENE_NAME] = new SceneConfigExample();
            //инициализируем крохотный список сцен)))
        }
    }
}

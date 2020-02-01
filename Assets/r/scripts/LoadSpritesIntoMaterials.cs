using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

#if UNITY_EDITOR
    using UnityEditor;
#endif

namespace Rick {

[ExecuteInEditMode]
public class LoadSpritesIntoMaterials : MonoBehaviour
{
    public Texture2D texture;
    public Sprite[] subsprites;

    [System.Serializable]
    public class MaterialBinding {
        public Material material;
        public Sprite sprite;

        public MaterialBinding(){}
    }
    
    [SerializeField] MaterialBinding[] bindings;

    bool bind = false;
    string tex = "";

    // Update is called once per frame
    void Update()
    {
        if(!Application.isPlaying){
            if(texture == null || tex != texture.name){
                tex = (texture == null)? "":texture.name;
                bind = false;
            }
            else
                bind = true;

            if(texture == null){
                subsprites = new Sprite[]{};
                ClearMaterials();
            }
            else {
                if(!bind){
                    #if UNITY_EDITOR
                        var path = AssetDatabase.GetAssetPath(texture);
                        subsprites = AssetDatabase.LoadAllAssetsAtPath(path).OfType<Sprite>().ToArray();
                    #endif

                    ClearMaterials();
                    BindMaterials();
                }
            }
        }
        else
            bind = true;

        ParseMaterials();
    }

    void ClearMaterials(){
        bindings = new MaterialBinding[]{};
    }

    void BindMaterials(){
        if(subsprites != null && subsprites.Length > 0){
            List<MaterialBinding> _bindings = new List<MaterialBinding>();
            
            foreach(Sprite s in subsprites){
                if(s != null){
                    var b = new MaterialBinding();
                        b.sprite = s;
                        b.material = null;

                    _bindings.Add(b);
                }
            }
            bindings = _bindings.ToArray();
        }
    }

    void ParseMaterials(){
        MaterialBinding mb;
        for(int i = 0; i < bindings.Length; i++){
            mb = bindings[i];

            var sprite = mb.sprite;
            if(mb.material != null){
                var croppedTexture = new Texture2D( (int)sprite.rect.width, (int)sprite.rect.height );
                var pixels = sprite.texture.GetPixels(  (int)sprite.textureRect.x, 
                                                        (int)sprite.textureRect.y, 
                                                        (int)sprite.textureRect.width, 
                                                        (int)sprite.textureRect.height );
                croppedTexture.SetPixels( pixels );
                croppedTexture.Apply();

                mb.material.mainTexture = croppedTexture;
            }
        }
    }
}

}

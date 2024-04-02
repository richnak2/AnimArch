using UnityEngine;
using System.IO;

namespace Visualization.UI{
    public class PatternCatalogueFileBrowser : MonoBehaviour
    {
        Component patternCatalogueComponent = new Composite("PatternCatalogue");
        public void Browse()
        {
            string folderPath = "Assets/Resources/PatternCatalogue";

            if (Directory.Exists(folderPath)){
                Debug.Log("Folder exists: " + folderPath);
                RecursivelyListFiles(folderPath);
            }else{
                Debug.LogError("Folder does not exist: " + folderPath);
            }
        }

        void RecursivelyListFiles(string folderPath)
        {   
            string[] files = Directory.GetFiles(folderPath);
            foreach (string file in files){
                if(!file.Contains(".meta")){
                    patternCatalogueComponent.Add(new Leaf(Path.GetFileName(file)));
                    Debug.Log("File found: " + Path.GetFileName(file));
                }
            }

            string[] subFolders = Directory.GetDirectories(folderPath);
            foreach (string subFolder in subFolders){
                Debug.Log("Subfolder found: " + subFolder);
                patternCatalogueComponent.Add(new Composite(Path.GetDirectoryName(subFolder)));
                RecursivelyListFiles(subFolder);
                
            }
        }
    }
}


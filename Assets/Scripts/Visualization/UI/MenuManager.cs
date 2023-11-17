using System;
using System.Collections.Generic;
using Assets.Scripts.Util;
using OALProgramControl;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Visualisation.Animation;
using Visualization.Animation;
using Visualization.ClassDiagram;
using Visualization.ClassDiagram.ClassComponents;
using Visualization.ClassDiagram.ComponentsInDiagram;

namespace Visualization.UI
{
    public class MenuManager : Singleton<MenuManager>
    {
        FileLoader fileLoader;

        //UI Panels
        [SerializeField] private GameObject introScreen;
        [SerializeField] private GameObject animationScreen;
        [SerializeField] private GameObject mainScreen;
        [SerializeField] private Button saveBtn;
        [SerializeField] private TMP_Dropdown animationsDropdown;
        [SerializeField] private TMP_InputField scriptCode;
        [SerializeField] private GameObject PanelColors;
        [SerializeField] private GameObject PanelInteractiveIntro;
        [SerializeField] private GameObject PanelInteractive;
        [SerializeField] private GameObject PanelMethod;
        [SerializeField] public Button generatePythonBtn;
        public bool isCreating = false;
        [SerializeField] private List<GameObject> methodButtons;
        [SerializeField] private TMP_Text ClassNameTxt;
        [SerializeField] private GameObject InteractiveText; // Modify to have static text - Click class in the diagram to start editing (InteractiveText.GetComponent<DotsAnimation>().currentText)
        [SerializeField] private GameObject PanelInteractiveShow; // To be deleleted
        [SerializeField] private TMP_Text classFromTxt; // To be deleleted
        [SerializeField] private TMP_Text classToTxt; // To be deleleted
        [SerializeField] private TMP_Text methodFromTxt; // To be deleleted
        [SerializeField] private TMP_Text methodToTxt; // To be deleleted
        [SerializeField] private GameObject PanelInteractiveCompleted;
        [SerializeField] private Slider speedSlider;
        [SerializeField] private TMP_Text speedLabel;
        private string interactiveSource = "source";
        private string sourceClassName = "";
        [SerializeField] public GameObject panelAnimationPlay;
        [SerializeField] public GameObject panelStepMode;
        [SerializeField] public GameObject panelPlayMode;
        [SerializeField] private TMP_InputField sepInput;
        [SerializeField] private TMP_Text classTxt;
        [SerializeField] private TMP_Text methodTxt;
        [SerializeField] private Toggle hideRelToggle;
        [SerializeField] public GameObject PanelChooseAnimationStartMethod;
        [SerializeField] public GameObject PanelSourceCodeAnimation;
        [SerializeField] public GameObject ShowErrorBtn;
        [SerializeField] public GameObject ErrorPanel;
        public Anim createdAnim;
        public bool isPlaying = false;
        public Button[] playBtns;
        public GameObject playIntroTexts;
        public List<AnimMethod> animMethods;
        public bool isSelectingNode;
        // executed on pressing show error button
        public void ShowErrorPanel()
        {
            ShowErrorPanel(null);
        }
        public void ShowErrorPanel(EXEExecutionResult executionResult = null) {
            ShowErrorBtn.GetComponent<Button>().interactable = false;
            ErrorPanel.SetActive(true);
            ErrorPanel.GetComponent<ExecutionErrorPanel>().FillPanel(executionResult);
        }
        // executed on pressing X button
        public void HideErrorPanel() {
            ShowErrorBtn.GetComponent<Button>().interactable = true;
            ErrorPanel.SetActive(false);
        }
        // executed after stopping or rerunning animation
        public void HideErrorPanelOnStopButton() {
            Debug.Log("Error panel Removed!");
            ErrorPanel.SetActive(false);
            ShowErrorBtn.GetComponent<Button>().interactable = false;
        }


        class InteractiveData
        {
            public Subject<string> ClassClickedInClassDiagram;
            public Subject<string> CurrentMethodOwningClass;
            public Subject<string> CurrentMethod;

            public InteractiveData()
            {
                this.ClassClickedInClassDiagram = new Subject<string>(string.Empty);
                this.CurrentMethodOwningClass = new Subject<string>(string.Empty);
                this.CurrentMethod = new Subject<string>(string.Empty);
            }

            public void Clear()
            {
                this.ClassClickedInClassDiagram.SetValue(string.Empty);
                this.CurrentMethodOwningClass.SetValue(string.Empty);
                this.CurrentMethod.SetValue(string.Empty);
            }
        }


        InteractiveData interactiveData = new InteractiveData();

        private void Awake()
        {
            fileLoader = GameObject.Find("FileManager").GetComponent<FileLoader>();

            this.interactiveData = new InteractiveData();
            this.interactiveData.ClassClickedInClassDiagram.Register((string value) => { ClassNameTxt.text = value; });
            this.interactiveData.CurrentMethodOwningClass.Register((string value) => { classTxt.text = value; });
            this.interactiveData.CurrentMethod.Register((string value) => { methodTxt.text = value; });
        }

        private void Start()
        {
            UpdateSpeed();
        }

        //Update the list of created animations
        public void UpdateAnimations()
        {
            List<string> options = new List<string>();
            foreach (Anim anim in AnimationData.Instance.getAnimList())
            {
                options.Add(anim.AnimationName);
            }

            if (animationsDropdown != null)
            {
                animationsDropdown.ClearOptions();
                animationsDropdown.AddOptions(options);
            }
        }

        public void SetSelectedAnimation(string name)
        {
            animationsDropdown.value = animationsDropdown.options.FindIndex(option => option.text == name);
        }

        public void InitializeAnim()
        {
            createdAnim = new Anim("");
            createdAnim.Initialize();
            generatePythonBtn.interactable = true;
        }

        public void StartAnimate()
        {
            scriptCode.text = string.Empty;
            isCreating = true;
            introScreen.SetActive(false);
            PanelInteractiveIntro.SetActive(true);
            PanelMethod.SetActive(false);
            PanelInteractive.SetActive(true);
            PanelInteractiveCompleted.SetActive(false);
            animationScreen.SetActive(true);
            mainScreen.SetActive(false);
        }

        public void EndAnimate()
        {
            isCreating = false;
            PanelInteractiveIntro.SetActive(false);
            PanelInteractive.SetActive(false);
            animationScreen.SetActive(false);
            saveBtn.interactable = false;
            mainScreen.SetActive(true);
            introScreen.SetActive(true);
            PanelInteractiveCompleted.SetActive(false);
        }

        public void SelectClass(String name)
        {
            // Save animation code
            if
            (
                !string.IsNullOrEmpty(interactiveData.CurrentMethodOwningClass.GetValue())
                && !string.IsNullOrEmpty(interactiveData.CurrentMethod.GetValue())
            )
            {
                createdAnim.SetMethodCode
                (
                    interactiveData.CurrentMethodOwningClass.GetValue(),
                    interactiveData.CurrentMethod.GetValue(),
                    sepInput.text
                );
            }

            // Unhighlight
            UnselectMethod();
            UnselectClass();

            // Set and highlight currently selected classs
            interactiveData.ClassClickedInClassDiagram.SetValue(name);
            Animation.Animation.Instance.HighlightClass(interactiveData.ClassClickedInClassDiagram.GetValue(), true);

            foreach (GameObject button in methodButtons)
            {
                button.SetActive(false);
            }

            // Setup method buttons
            Class selectedClass = DiagramPool.Instance.ClassDiagram.FindClassByName(name).ParsedClass;
            PanelInteractiveIntro.SetActive(false);
            
            PanelMethod.SetActive(true);
            int i = 0;
            if (selectedClass.Methods != null)
            {
                foreach (Method m in selectedClass.Methods)
                {
                    if (i < methodButtons.Count)
                    {
                        methodButtons[i].SetActive(true);
                        methodButtons[i].GetComponentInChildren<TMP_Text>().text = m.Name + "()";
                    }

                    i++;
                }
            }

            PanelInteractiveIntro.SetActive(false);
            PanelMethod.SetActive(true);
        }

        private void UnselectClass()
        {
            if (!string.IsNullOrEmpty(interactiveData.ClassClickedInClassDiagram.GetValue()))
            {
                Animation.Animation.Instance.HighlightClass(interactiveData.ClassClickedInClassDiagram.GetValue(), false);
            }

            if (!string.IsNullOrEmpty(interactiveData.CurrentMethodOwningClass.GetValue()))
            {
                Animation.Animation.Instance.HighlightClass(interactiveData.CurrentMethodOwningClass.GetValue(), false);
            }
        }

        public void SelectMethod(int buttonID)
        {

            string methodName = methodButtons[buttonID].GetComponentInChildren<TMP_Text>().text;

            interactiveData.CurrentMethodOwningClass.SetValue(interactiveData.ClassClickedInClassDiagram.GetValue());
            interactiveData.ClassClickedInClassDiagram.SetValue(string.Empty);
            interactiveData.CurrentMethod.SetValue(methodName);

            Animation.Animation.Instance.HighlightMethod
            (
                interactiveData.CurrentMethodOwningClass.GetValue(),
                interactiveData.CurrentMethod.GetValue(),
                true
            );
            sepInput.interactable = true;
            sepInput.text
                = createdAnim.GetMethodBody
                (
                    interactiveData.CurrentMethodOwningClass.GetValue(),
                    interactiveData.CurrentMethod.GetValue()
                );

            PanelInteractiveIntro.SetActive(true);
            PanelMethod.SetActive(false);
        }

        private void UnselectMethod()
        {
            if (!string.IsNullOrEmpty(interactiveData.CurrentMethod.GetValue()))
            {
                Animation.Animation.Instance.HighlightMethod(interactiveData.CurrentMethodOwningClass.GetValue(), interactiveData.CurrentMethod.GetValue(), false);
            }
        }

        //Save animation to file and memory
        public void SaveAnimation()
        {
            if (sepInput.text.Length > 2 && !classTxt.text.Equals("class unselected") &&
                !methodTxt.text.Equals("method unselected"))
            {
                createdAnim.SetMethodCode(classTxt.text, methodTxt.text, sepInput.text);
            }

            scriptCode.gameObject.SetActive(true);

            scriptCode.GetComponent<CodeHighlighter>().RemoveColors();
            createdAnim.Code = scriptCode.text;
            scriptCode.gameObject.SetActive(false);
            fileLoader.SaveAnimation(createdAnim);
            EndAnimate();
        }

        public void SelectAnimation()
        {
            String name = animationsDropdown.options[animationsDropdown.value].text;
            foreach (Anim anim in AnimationData.Instance.getAnimList())
            {
                if (name.Equals(anim.AnimationName))
                    AnimationData.Instance.selectedAnim = anim;
            }
        }

        public void OpenAnimation()
        {
            if (AnimationData.Instance.getAnimList().Count > 0)
            {
                SelectAnimation();
                StartAnimate();
                createdAnim = AnimationData.Instance.selectedAnim;
                scriptCode.text = createdAnim.Code;
                scriptCode.text = AnimationData.Instance.selectedAnim.Code;
                AnimationData.Instance.RemoveAnim(AnimationData.Instance.selectedAnim);
                UpdateAnimations();
            }
        }

        public void ActivatePanelColors(bool show)
        {
            PanelColors.SetActive(show);
        }

        public void UpdateSpeed()
        {
            if (speedSlider && speedLabel)
            {
                AnimationData.Instance.AnimSpeed = speedSlider.value;
                speedLabel.text = speedSlider.value.ToString() + "s";
            }
        }

        public void PlayAnimation()
        {
            Debug.Assert(!AnimationData.Instance.selectedAnim.AnimationName.Equals(""));

            PanelChooseAnimationStartMethod.SetActive(true);
            PanelSourceCodeAnimation.SetActive(false);

            isPlaying = true;
            panelAnimationPlay.SetActive(true);
            mainScreen.SetActive(false);
            introScreen.SetActive(false);
            foreach (Button button in playBtns)
            {
                button.gameObject.SetActive(false);
            }

            playIntroTexts.SetActive(true);
            if (Animation.Animation.Instance.standardPlayMode)
            {
                panelStepMode.SetActive(false);
                panelPlayMode.SetActive(true);
            }
            else
            {
                panelPlayMode.SetActive(false);
                panelStepMode.SetActive(true);
            }
        }

        public void ResetInteractiveSelection()
        {
            if (!string.IsNullOrEmpty(interactiveData.CurrentMethodOwningClass.GetValue()))
            {
                Animation.Animation.Instance.HighlightClass(interactiveData.CurrentMethodOwningClass.GetValue(), false);
            }

            if (interactiveData.CurrentMethod.GetValue() != null)
            {
                Animation.Animation.Instance.HighlightMethod
                (
                    interactiveData.CurrentMethodOwningClass.GetValue(),
                    interactiveData.CurrentMethod.GetValue(),
                    false
                );
            }

            interactiveData.Clear();
            PanelInteractiveIntro.SetActive(true);
            PanelMethod.SetActive(false);
        }

        public void ChangeMode()
        {
            Animation.Animation.Instance.UnhighlightAll();
            Animation.Animation.Instance.isPaused = false;
            if (Animation.Animation.Instance.standardPlayMode)
            {
                Animation.Animation.Instance.standardPlayMode = false;
                panelPlayMode.SetActive(false);
                panelStepMode.SetActive(true);
            }
            else
            {
                Animation.Animation.Instance.standardPlayMode = true;
                panelStepMode.SetActive(false);
                panelPlayMode.SetActive(true);
            }
        }

        public void SelectPlayClass(string name)
        {
            DiagramPool.Instance.ObjectDiagram.ResetDiagram();
            DiagramPool.Instance.ObjectDiagram.LoadDiagram();

            Animation.Animation.Instance.UnhighlightAll();
            Animation.Animation.Instance.HighlightClass(name, true);

            playIntroTexts.SetActive(false);
            Animation.Animation.Instance.startClassName = name;
            foreach (Button button in playBtns)
            {
                button.gameObject.SetActive(false);
            }

            Class selectedClass = DiagramPool.Instance.ClassDiagram.FindClassByName(name).ParsedClass;
            animMethods = AnimationData.Instance.selectedAnim.GetMethodsByClassName(name);
            int i = 0;
            if (animMethods != null)
            {
                foreach (AnimMethod m in animMethods)
                {
                    if (i < 4)
                    {
                        playBtns[i].GetComponentInChildren<TMP_Text>().text = m.Name + "()";
                        playBtns[i].gameObject.SetActive(true);
                        i++;
                    }
                }
            }
        }

        public void SelectPlayMethod(int id)
        {
            Animation.Animation.Instance.startMethodName = animMethods[id].Name;
            foreach (Button button in playBtns)
            {
                button.gameObject.SetActive(false);
            }

            playIntroTexts.SetActive(true);
            Debug.Log("Selected class: " + Animation.Animation.Instance.startClassName + "Selected Method: " +
                      Animation.Animation.Instance.startMethodName);
            Animation.Animation.Instance.HighlightClass(Animation.Animation.Instance.startClassName, false);
        }

        public void UnshowAnimation()
        {
            Animation.Animation.Instance.UnhighlightAll();
        }

        public void EndPlay()
        {
            isPlaying = false;
            foreach (Button button in playBtns)
            {
                button.gameObject.SetActive(false);
            }

            Animation.Animation.Instance.startClassName = "";
            Animation.Animation.Instance.startMethodName = "";
        }

        public void HideGraphRelations()
        {
            if (hideRelToggle.isOn)
            {
                foreach (var interGraphRelation in DiagramPool.Instance.RelationsClassToObject)
                {
                    interGraphRelation.Show();
                }
            }
            else
            {
                foreach (var interGraphRelation in DiagramPool.Instance.RelationsClassToObject)
                {
                    interGraphRelation.Hide();
                }
            }
        }

        public static void SetAnimationButtonsActive(bool active)
        {
            GameObject.Find("MainPanel").transform.Find("Edit").GetComponentInChildren<Button>().interactable = active;
            GameObject.Find("MainPanel").transform.Find("Play").GetComponentInChildren<Button>().interactable = active;
            GameObject.Find("MainPanel").transform.Find("AnimationSelect").GetComponentInChildren<TMP_Dropdown>().interactable = active;
        }
        public void AnimateSourceCodeAtMethodStart(EXEScopeMethod currentMethodScope)
        {
            PanelChooseAnimationStartMethod.SetActive(false);
            PanelSourceCodeAnimation.SetActive(true);

            PanelSourceCodeAnimation
                .GetComponent<PanelSourceCodeAnimation>()
                .SetMethodLabelText(currentMethodScope.MethodDefinition.OwningClass.Name, currentMethodScope.MethodDefinition.Name);

            string sourceCode = currentMethodScope.ToFormattedCode();
            PanelSourceCodeAnimation.GetComponent<PanelSourceCodeAnimation>().SetSourceCodeText(sourceCode);
        }
    }
}

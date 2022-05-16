using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Web;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using Assets.Scripts.Logger;
using Assets.Map.WorldMap;

public class MainMenuScript : MonoBehaviour
{
    public MainMap mapPrefab;

    public InputField chunkCountXField;
    public InputField chunkCountYField;

    public Slider tropicTreeProcentSlider;
    public Slider standartTreeProcentSlider;
    public Slider winterTreeProcentSlider;

    public InputField tropicTreeProcentField;
    public InputField standartTreeProcentField;
    public InputField winterTreeProcentField;

    public Slider mixingBiomesSlider;
    public InputField mixingBiomesField;

    public InputField seedField;
    public Button seedGenerateBtn;

    public InputField seed_field;

    public Texture2D _texture;
    public Image mapImage;

    public Slider waterLevelSlider;
    public Slider rockLevelSlider;


    public MenuManager menuManager;

    private void Awake()
    {
        OpenGenerationMenu();
    }

    public void ExitBtnPressed()
    {
        Application.Quit();
    }
    public void EnterGame()
    {
        UnityEngine.Random.InitState(GlobalVariables.generationSettings.Seed);
        Loger.Log("Logs\\MainMenu.log", "Entering game...");
        SceneManager.LoadScene("SampleScene");
    }

    public void SwitchTropicTreeProcentFieldValue()
    {
        tropicTreeProcentField.text = tropicTreeProcentSlider.value.ToString();
        GlobalVariables.generationSettings.tropicTreeProcent = Convert.ToInt32(tropicTreeProcentField.text);
    }

    public void SwitchTropicTreeProcentSliderValue()
    {
        tropicTreeProcentSlider.value = Convert.ToInt32(tropicTreeProcentField.text);
    }

    public void SwitchStandartTreeProcentFieldValue()
    {
        standartTreeProcentField.text = standartTreeProcentSlider.value.ToString();
        GlobalVariables.generationSettings.standartTreeProcent = Convert.ToInt32(standartTreeProcentField.text);
    }

    public void SwitchStandartTreeProcentSliderValue()
    {
        standartTreeProcentSlider.value = Convert.ToInt32(standartTreeProcentField.text);
    }

    public void SwitchWinterTreeProcentFieldValue()
    {
        winterTreeProcentField.text = winterTreeProcentSlider.value.ToString();
        GlobalVariables.generationSettings.winterTreeProcent = Convert.ToInt32(winterTreeProcentField.text);
    }

    public void SwitchWinterTreeProcentSliderValue()
    {
        winterTreeProcentSlider.value = Convert.ToInt32(winterTreeProcentField.text);
    }

    public void SwitchXField()
    {
        GlobalVariables.generationSettings.terrainChunkCountX = Convert.ToInt32(chunkCountXField.text);
    }

    public void SwitchYField()
    {
        GlobalVariables.generationSettings.terrainChunkCountY = Convert.ToInt32(chunkCountYField.text);
    }


    public void SwitchMixingBiomesFieldValue()
    {
        mixingBiomesField.text = mixingBiomesSlider.value.ToString();
    }

    public void SwitchMixingBiomesSliderValue()
    {
        try
        {
            mixingBiomesSlider.value = Convert.ToInt32(mixingBiomesField.text);
        }
        catch
        {
            mixingBiomesSlider.value = Convert.ToInt32(mixingBiomesField.text = "3");
        }
    }

    public void SwitchWaterHeightSLider()
    {
        GlobalVariables.generationSettings.waterHeihgt = waterLevelSlider.value;
        GlobalVariables.generationSettings.sandHeight = GlobalVariables.generationSettings.waterHeihgt + 0.05f;
        GlobalVariables.generationSettings.terrainHeight = GlobalVariables.generationSettings.sandHeight + 0.5f;
        RedrawMap();
    }

    public void SwitchRockHeightSlider()
    {
        GlobalVariables.generationSettings.rockHeight = rockLevelSlider.value;
        RedrawMap();
    }

    public void SwitchSeedFieldValue()
    {
        GlobalVariables.generationSettings.Seed = Convert.ToInt32(seedField.text);
        UnityEngine.Random.InitState(GlobalVariables.generationSettings.Seed);
        GlobalVariables.generationSettings.heightMatrix = new float[GlobalVariables.generationSettings.terrainChunkCountX * 3, GlobalVariables.generationSettings.terrainChunkCountY * 3];
        HexFieldGenerator.GenerateHexMap(GlobalVariables.generationSettings.heightMatrix);
        RedrawMap();
    }

    public void GenerateSeedOnClick()
    {
        GlobalVariables.generationSettings.Seed = new System.Random().Next(3000000);
        seedField.text = GlobalVariables.generationSettings.Seed.ToString();
        GlobalVariables.generationSettings.heightMatrix = new float[GlobalVariables.generationSettings.terrainChunkCountX * 3, GlobalVariables.generationSettings.terrainChunkCountY * 3];
        HexFieldGenerator.GenerateHexMap(GlobalVariables.generationSettings.heightMatrix);
        RedrawMap();
    }

    public void OpenGenerationMenu()
    {
        menuManager.ShowMenu("Generation");
        GlobalVariables.generationSettings.terrainChunkCountX = 35;
        GlobalVariables.generationSettings.terrainChunkCountY = 21;

        GlobalVariables.generationSettings.tropicTreeProcent = 50;
        GlobalVariables.generationSettings.standartTreeProcent = 50;
        GlobalVariables.generationSettings.winterTreeProcent = 50;
        GlobalVariables.generationSettings.mixingBiomesCount = 3;

        GlobalVariables.generationSettings.Seed = new System.Random().Next(3000000);

        GlobalVariables.generationSettings.rockHeight = 0.5f;
        GlobalVariables.generationSettings.waterHeihgt = 0.2f;
        GlobalVariables.generationSettings.sandHeight = GlobalVariables.generationSettings.waterHeihgt + 0.05f;
        GlobalVariables.generationSettings.terrainHeight = GlobalVariables.generationSettings.sandHeight + 0.5f;

        seedField.text = GlobalVariables.generationSettings.Seed.ToString();

        GlobalVariables.generationSettings.heightMatrix = new float[GlobalVariables.generationSettings.terrainChunkCountX * 3, GlobalVariables.generationSettings.terrainChunkCountY * 3];
        HexFieldGenerator.GenerateHexMap(GlobalVariables.generationSettings.heightMatrix);
        RedrawMap();
    }

    private void RedrawMap()
    {
        _texture = new Texture2D(GlobalVariables.generationSettings.terrainChunkCountX * 3, GlobalVariables.generationSettings.terrainChunkCountY * 3);
        Sprite sprite = Sprite.Create(_texture, new Rect(0, 0, _texture.width, _texture.height), new Vector2(0.0f, 1.0f), pixelsPerUnit: 1f);
        //var spriteRenderer = GetComponent<SpriteRenderer>();
        //spriteRenderer.sprite = sprite;

        for (int i = 0; i < GlobalVariables.generationSettings.terrainChunkCountX * 3; i++)
        {
            for (int j = 0; j < GlobalVariables.generationSettings.terrainChunkCountY * 3; j++)
            {
                var height = GlobalVariables.generationSettings.heightMatrix[i, j];
                Color pixelColor;
                if(height < GlobalVariables.generationSettings.waterHeihgt)
                {
                    pixelColor = Color.blue;
                }
                else if(height < GlobalVariables.generationSettings.sandHeight)
                {
                    pixelColor = Color.yellow;
                }
                else if(height < GlobalVariables.generationSettings.terrainHeight)
                {
                    pixelColor = Color.green;
                }
                else if(height < GlobalVariables.generationSettings.rockHeight)
                {
                    var color1 = new Color(46f / 255, 182f / 255, 44f / 255);
                    var color2 = new Color(0f / 255, 125f / 255, 0f / 255);
                    var lerpValue = Color.Lerp(color1, color2, height / GlobalVariables.generationSettings.rockHeight);
                    pixelColor = lerpValue;
                }
                else
                {
                    pixelColor = new Color(94f / 255, 103f / 255, 109f / 255);
                }
                _texture.SetPixel(i, j, pixelColor);
            }

        }
        _texture.Apply();

        mapImage.sprite = sprite;
        //mapImage.SetNativeSize();
        //Vector2 tmp = new Vector2(GlobalVariables.generationSettings.terrainChunkCountX * 3, GlobalVariables.generationSettings.terrainChunkCountY * 3);
        //mapImage.transform.localScale = tmp;
                
    }
}

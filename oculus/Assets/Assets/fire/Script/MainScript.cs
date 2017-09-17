using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class MainScript : MonoBehaviour {

    public GameObject rightHand;
    public GameObject leftHand;
    public GameObject WildFire;
    public GameObject FireComplex;
    public GameObject effect;

    private int fireCount = 0;
    private float time = 0f;

    private Rigidbody rigidbodyLeftHand;
    private Vector3 nowLeftHand;
    private Vector3 prevLeftHand;


    // Use this for initialization
    void Start () {
        time = 0;
        rigidbodyLeftHand = leftHand.GetComponent<Rigidbody>();
        nowLeftHand = leftHand.transform.position;
        prevLeftHand = leftHand.transform.position;
    }
	
	// Update is called once per frame
	void Update () {

        // time
        time += Time.deltaTime;
        nowLeftHand = leftHand.transform.position;

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Rキーで位置トラッキングをリセットする");
            InputTracking.Recenter();
        }

        SetButtonMapping();
        SetStickMapping();

        prevLeftHand = leftHand.transform.position;
        // 顔の動き
        Vector3 position = InputTracking.GetLocalPosition(VRNode.CenterEye);
        Quaternion rotation = InputTracking.GetLocalRotation(VRNode.CenterEye);
    }

    void SetButtonMapping() {

        if (OVRInput.GetDown(OVRInput.RawButton.A))
        {
            Debug.Log("Aボタンを押した");
        }
        if (OVRInput.GetDown(OVRInput.RawButton.B))
        {
            Debug.Log("Bボタンを押した");
        }
        if (OVRInput.GetDown(OVRInput.RawButton.X))
        {
            Debug.Log("Xボタンを押した");
        }
        if (OVRInput.GetDown(OVRInput.RawButton.Y))
        {
            Debug.Log("Yボタンを押した");
        }
        if (OVRInput.GetDown(OVRInput.RawButton.Start))
        {
            Debug.Log("メニューボタン（左アナログスティックの下にある）を押した");
        }
        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
        {
            Debug.Log("右人差し指トリガーを押した");
            CreateRightFire();
        }
        if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger)) {
            Debug.Log("右人差し指トリガーを離した");
        }
        if (OVRInput.GetDown(OVRInput.RawButton.RHandTrigger))
        {
            Debug.Log("右中指トリガーを押した");
        }
        if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
        {
            Debug.Log("左人差し指トリガーを押した");
            CreateLeftFire();
        }
        if (OVRInput.GetUp(OVRInput.RawButton.LIndexTrigger))
        {
            Debug.Log("左人差し指トリガーを離した");
            ShotFireLeft();
        }
        if (OVRInput.GetDown(OVRInput.RawButton.LHandTrigger))
        {
            Debug.Log("左中指トリガーを押した");
        }
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            Debug.Log("選択した");
        }
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            Debug.Log("キャンセルした");
        }

    }

    void SetStickMapping() {
   
        // 左手のアナログスティックの向きを取得
        Vector2 stickL = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick);
        if (OVRInput.GetDown(OVRInput.RawButton.LThumbstick))
        {
            Debug.Log("左アナログスティックを押し込んだ");
        }
        if (OVRInput.GetDown(OVRInput.RawButton.LThumbstickUp))
        {
            Debug.Log("左アナログスティックを上に倒した");
        }
        if (OVRInput.GetDown(OVRInput.RawButton.LThumbstickDown))
        {
            Debug.Log("左アナログスティックを下に倒した");
        }
        if (OVRInput.GetDown(OVRInput.RawButton.LThumbstickLeft))
        {
            Debug.Log("左アナログスティックを左に倒した");
        }
        if (OVRInput.GetDown(OVRInput.RawButton.LThumbstickRight))
        {
            Debug.Log("左アナログスティックを右に倒した");
        }

        // 右手のアナログスティックの向きを取得
        Vector2 stickR = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick);
        if (OVRInput.GetDown(OVRInput.RawButton.RThumbstick))
        {
            Debug.Log("右アナログスティックを押し込んだ");
        }
        if (OVRInput.GetDown(OVRInput.RawButton.RThumbstickUp))
        {
            Debug.Log("右アナログスティックを上に倒した");
        }
        if (OVRInput.GetDown(OVRInput.RawButton.RThumbstickDown))
        {
            Debug.Log("右アナログスティックを下に倒した");
        }
        if (OVRInput.GetDown(OVRInput.RawButton.RThumbstickLeft))
        {
            Debug.Log("右アナログスティックを左に倒した");
        }
        if (OVRInput.GetDown(OVRInput.RawButton.RThumbstickRight))
        {
            Debug.Log("右アナログスティックを右に倒した");
        }

    }

    void CreateRightFire()
    {

        Vector3 rightHandPos = rightHand.transform.position;
        GameObject rightFire = (GameObject)Instantiate(FireComplex, rightHandPos, Quaternion.identity);
        rightFire.transform.parent = effect.transform;
        rightFire.name = "rightFire";
        HandFireScript handScript = rightFire.GetComponent<HandFireScript>();
        handScript.SetHand(rightHand);

    }

    void CreateLeftFire()
    {

        Vector3 leftHandPos = leftHand.transform.position;
        GameObject leftFire = (GameObject)Instantiate(FireComplex, leftHandPos, Quaternion.identity);
        leftFire.transform.parent = effect.transform;
        leftFire.name = "leftFire";
        HandFireScript handScript = leftFire.GetComponent<HandFireScript>();
        handScript.SetHand(leftHand);

    }

    void ShotFireLeft() {

        float x = 1000 * (nowLeftHand.x - prevLeftHand.x);
        float y = 1000 * (nowLeftHand.y - prevLeftHand.y);
        float z = 1000 * (nowLeftHand.z - prevLeftHand.z);
        float dist = Vector3.Distance(nowLeftHand, prevLeftHand) * 1000;
        // Debug.Log("dist = " + dist);

        GameObject rightFire = (GameObject)Instantiate(WildFire, nowLeftHand, Quaternion.identity);
        rightFire.transform.parent = effect.transform;
        rightFire.name = "rightShot";
        FireShot fireShot = rightFire.GetComponent<FireShot>();
        fireShot.Force(x, y, z);

    }

    public float GetTime() {
        return time;
    }
}

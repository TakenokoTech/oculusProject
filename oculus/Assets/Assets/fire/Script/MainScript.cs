using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class MainScript : MonoBehaviour {

    // Oculus
    public GameObject head;
    public GameObject rightHand;
    public GameObject leftHand;
    
    // オブジェクト
    public GameObject WildFire;
    public GameObject FireComplex;
    public GameObject effect;

    // ユニティちゃん
    public GameObject UnitychanHead;
    public GameObject UnitychanRightHand;
    public GameObject UnitychanLeftHand;

    // 環境変数
    private int fireCount = 0;
    private float time = 0f;

    // 頭
    private Vector3 nowHead;
    private Vector3 prevHead;

    // 左手
    //private Rigidbody rigidbodyLeftHand;
    private Vector3 nowLeftHand;
    private Vector3 prevLeftHand;
    private GameObject leftFire;

    // 右手
    //private Rigidbody rigidbodyRightHand;
    private Vector3 nowRightHand;
    private Vector3 prevRightHand;
    private GameObject rightFire;
    

    // Use this for initialization
    void Start () {
        time = 0;

        nowHead = head.transform.position;
        prevHead = head.transform.position;

        // rigidbodyLeftHand = leftHand.GetComponent<Rigidbody>();
        nowLeftHand = leftHand.transform.position;
        prevLeftHand = leftHand.transform.position;

        //rigidbodyRightHand = rightHand.GetComponent<Rigidbody>();
        nowRightHand = rightHand.transform.position;
        prevRightHand = rightHand.transform.position;

    }
	
	// Update is called once per frame
	void Update () {

        // time
        time += Time.deltaTime;

        nowHead = head.transform.position;
        nowRightHand = rightHand.transform.position;
        nowLeftHand = leftHand.transform.position;

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Rキーで位置トラッキングをリセットする");
            InputTracking.Recenter();
        }

        SetButtonMapping();
        SetStickMapping();

        prevHead = head.transform.position;
        prevRightHand = rightHand.transform.position;
        prevLeftHand = leftHand.transform.position;

        // 手を反映
        UnitychanHead.transform.position = head.transform.position;
        UnitychanHead.transform.rotation = head.transform.rotation;
        UnitychanRightHand.transform.position = rightHand.transform.position;
        UnitychanRightHand.transform.rotation = rightHand.transform.rotation;
        UnitychanLeftHand.transform.position = leftHand.transform.position;
        UnitychanLeftHand.transform.rotation = leftHand.transform.rotation;

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
            ShotFireRight();
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
        if (rightFire == null)
        {
            Vector3 rightHandPos = rightHand.transform.position;
            rightFire = (GameObject)Instantiate(FireComplex, rightHandPos, Quaternion.identity);
            rightFire.transform.parent = effect.transform;
            rightFire.name = "rightFire";
            HandFireScript handScript = rightFire.GetComponent<HandFireScript>();
            handScript.SetHand(rightHand);
        }

    }

    void CreateLeftFire()
    {

        if (leftFire == null)
        {
            Vector3 leftHandPos = leftHand.transform.position;
            leftFire = (GameObject)Instantiate(FireComplex, leftHandPos, Quaternion.identity);
            leftFire.transform.parent = effect.transform;
            leftFire.name = "leftFire";
            HandFireScript handScript = leftFire.GetComponent<HandFireScript>();
            handScript.SetHand(leftHand);
        }
    }

    void ShotFireRight()
    {

        float x = 1000 * (nowRightHand.x - prevRightHand.x);
        float y = 1000 * (nowRightHand.y - prevRightHand.y);
        float z = 1000 * (nowRightHand.z - prevRightHand.z);
        float dist = Vector3.Distance(nowRightHand, prevRightHand) * 1000;

        GameObject wildFire = (GameObject)Instantiate(WildFire, nowRightHand, Quaternion.identity);
        wildFire.transform.parent = effect.transform;
        wildFire.name = "rightShot";
        FireShot fireShot = wildFire.GetComponent<FireShot>();
        fireShot.Force(x, y, z);

        Destroy(rightFire);
        rightFire = null;

    }

    void ShotFireLeft() {

        float x = 1000 * (nowLeftHand.x - prevLeftHand.x);
        float y = 1000 * (nowLeftHand.y - prevLeftHand.y);
        float z = 1000 * (nowLeftHand.z - prevLeftHand.z);
        float dist = Vector3.Distance(nowLeftHand, prevLeftHand) * 1000;

        GameObject wildFire = (GameObject)Instantiate(WildFire, nowLeftHand, Quaternion.identity);
        wildFire.transform.parent = effect.transform;
        wildFire.name = "leftShot";
        FireShot fireShot = wildFire.GetComponent<FireShot>();
        fireShot.Force(x, y, z);

        Destroy(leftFire);
        leftFire = null;

    }

    public float GetTime() {
        return time;
    }
}

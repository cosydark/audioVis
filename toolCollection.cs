using UnityEditor; //引用Unity编辑器命名空间
using UnityEngine; //引用Unity引擎命名空间
//
public class toolCollection : ScriptableWizard
{
    [Header("子父级")]
    public Transform Aduio_process;
    public Transform source;
    //
    [Header("网格和材质")]
    public Mesh Ref;
    public Material materialRef;
    //
    [Header("组设置")]
    public Transform parentGroup;
    [Header("================")]
    public bool mesh;
    public Mesh newMesh;
    public bool material;
    public Material newMaterial;
    public bool physicMaterial;
    public PhysicMaterial newpMaterial;
    [Header("全局设置")]
    public Transform head;
    public bool setColRadius;
    public float newColRadius;
    public bool mass;
    public float newmass;

    [MenuItem("全屏蔽别哔哔的工具/toolCollection")]//菜单栏创建
    static void 嘀嘀嘀()
    {
        //显示器向导 <所管控的类>（ 窗口的名字，窗口中按钮的名字,第二个按钮的名字 ）；
        DisplayWizard<toolCollection>("toolCollection", "退出", "Set");
    }
    void OnEnable()//固定函数名：窗口开始时执行
    {
    }
    void OnWizardCreate()// 固定函数名：对应现实向导中的第一个按钮：A
    {
    }
    void OnWizardOtherButton()// 固定函数名：对应现实向导中的第二按钮：B
    {
        //获取cube中心点，并整理
        GameObject newsource = new GameObject(source.name + "_new");
        for (int i = 0; i < source.childCount; i++)
        {
            GameObject centerPos = new GameObject(source.GetChild(i).name);
            source.GetChild(i).gameObject.AddComponent<BoxCollider>();
            centerPos.transform.position = source.GetChild(i).GetComponent<BoxCollider>().bounds.center;
            centerPos.transform.SetParent(newsource.transform, true);
        }
        DestroyImmediate(source.gameObject);
        newsource.transform.SetParent(Aduio_process);
        //为新创建的对象添加组件
        newsource.AddComponent<scaleGroup>();
        for (int i = 0; i < newsource.transform.childCount; i++)
        {
            newsource.transform.GetChild(i).gameObject.AddComponent<MeshFilter>().mesh = Ref;
            newsource.transform.GetChild(i).gameObject.AddComponent<MeshRenderer>().material = materialRef;
            newsource.transform.GetChild(i).gameObject.AddComponent<SphereCollider>().radius = 0.5f;
            newsource.transform.GetChild(i).gameObject.AddComponent<Rigidbody>();
        }
    }
    void OnWizardUpdate()//当属性值被修改时，每帧调用
    {
        if (mesh)
        {
            for (int i = 0; i < parentGroup.childCount; i++)
            {
                parentGroup.GetChild(i).gameObject.GetComponent<MeshFilter>().mesh = newMesh;
            }
            mesh = false;
        }
        //
        if (material)
        {
            for (int i = 0; i < parentGroup.childCount; i++)
            {
                parentGroup.GetChild(i).gameObject.GetComponent<MeshRenderer>().material = newMaterial;
            }
            material = false;
        }
        //
        if (physicMaterial)
        {
            for (int i = 0; i < parentGroup.childCount; i++)
            {
                parentGroup.GetChild(i).gameObject.GetComponent<SphereCollider>().material = newpMaterial;
            }
            physicMaterial = false;
        }
        //
        if (setColRadius)
        {
            for (int i = 0; i < head.childCount; i++)
            {
                for (int g = 0; g < head.GetChild(i).childCount; g++)
                {
                    head.GetChild(i).GetChild(g).GetComponent<SphereCollider>().radius = newColRadius;
                }
            }
            setColRadius = false;
        }
        if (mass)
        {
            for (int i = 0; i < head.childCount; i++)
            {
                for (int g = 0; g < head.GetChild(i).childCount; g++)
                {
                    head.GetChild(i).GetChild(g).GetComponent<Rigidbody>().mass = newmass;
                }
            }
            mass = false;
        }

    }
    void OnSelectionChange()//当选择的物体发生改变，调用此函数
    {

    }
}
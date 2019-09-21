##### 3dGame-hw2
牧师与恶魔视频见[爱奇艺]()
---
### 简答题
##### 1.游戏对象运动的本质是什么？
游戏对象运动的本质是游戏对象位置和状态的改变。通过游戏对象transform属性的position、rotation和scale等属性的变化来实现运动。
##### 2.请用三种方法以上方法，实现物体的抛物线运动。
* **修改Transform中的position属性直接改变位置**
抛物线运动水平方向上的移动距离为T，竖直方向上的移动距离为VT*T。
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class methodA : MonoBehaviour {

    public int Myspeed = 2;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += Vector3.right * Time.deltaTime;
        this.transform.position += Vector3.down * Time.deltaTime * Time.deltaTime * Myspeed;
    }
}
```
* **使用向量Vector3的MoveTowards方法**
方法原型为：
```
static function MoveTowards (current : Vector3, target : Vector3, maxDistanceDelta : float) : Vector3 
```
物体每一帧都在水平方向和竖直方向移动，与方法1同理。
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class methodB : MonoBehaviour {
    Vector3 target1 = Vector3.right * 5;
    Vector3 target2 = Vector3.down * 5;
    float speed1 = 1;
    float speed2 = 2;
    
    // Update is called once per frame
    void Update(){
        float step1 = speed1 * Time.deltaTime;
        float step2 = speed2 * Time.deltaTime * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target1, step1);
        transform.position = Vector3.MoveTowards(transform.position, target2, step2);
    }
}
```
* **使用向量Vector3的Lerp方法**
方法原型为：
```
static function Lerp (from : Vector3, to : Vector3, t : float) : Vector3
```
按照数字t在from到to之间插值。**t大小在 [0...1]之间**，当t = 0时，返回from，当t = 1时，返回to。当t = 0.5 返回from和to的平均数。
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class methodC : MonoBehaviour {
    public float Speed = 0.5f;
    Vector3 Target1 = new Vector3(-6, -3, 8);
    //控制物体向Target移动
    void Update()
    {
        gameObject.transform.localPosition = Vector3.Lerp(transform.position, Target1, Speed * Time.deltaTime);
    }
}
```
##### 3.写一个程序，实现一个完整的太阳系， 其他星球围绕太阳的转速必须不一样，且不在一个法平面上。
* 设计思路
(1) 首先设置10个球体，分别是太阳、八大行星和月球。八大行星按照离太阳的距离从近到远依次为水星、金星、地球、火星、木星、土星、天王星、海王星。

![在这里插入图片描述](https://img-blog.csdnimg.cn/20190920192459245.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3h1YW5fdGluZw==,size_16,color_FFFFFF,t_70)
(2)设置天体的自转和公转，包括地月系。由于题目需要行星要在不同的法平面旋转，我们随机设定行星旋转的轴位置，并且设置旋转速度的不同。以天王星的自转和公转为例。
自转：```public void Rotate(Vector3 eulers,Space relativeTo)```
公转：```public void RotateAround(Vector3 point,Vector3 axis,float angle)```
代码如下：
	```
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	
	public class tianwang : MonoBehaviour {
	    public Transform Sun;
	    public float rotationSpeed = 2;
	    public float speed = 8.5f;
	    private float rx;
	    private float ry;
	    private float rz;
	    // Use this for initialization
	    void Start()
	    {
	        rx = Random.Range(10, 30);
	        ry = Random.Range(40, 60);
	        rz = Random.Range(10, 30);
	    }
	
	    // Update is called once per frame
	    void Update()
	    {
	        Vector3 axis = new Vector3(rx, ry, rz);
	        this.transform.RotateAround(Sun.position, axis, speed * Time.deltaTime);
	        transform.Rotate(Vector3.down * rotationSpeed, Space.World);
	    }
	}
	```
	(3)设置摄像机在PLAY过程中也可以移动来观察天体运动。
	```
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	
	public class View : MonoBehaviour {
	    public float sensitivityMouse = 2f;
	    public float sensitivetyKeyBoard = 0.1f;
	    public float sensitivetyMouseWheel = 10f;
	
	    // Use this for initialization
	    void Start()
	    {
	
	    }
	
	    // Update is called once per frame
	    void Update()
	    {
	        //按着鼠标右键实现视角转动  
	        if (Input.GetMouseButton(1))
	        {
	            transform.Rotate(-Input.GetAxis("Mouse Y") * sensitivityMouse, Input.GetAxis("Mouse X") * sensitivityMouse, 0);
	        }
	
	        //键盘按钮←/a和→/d实现视角水平移动，键盘按钮↑/w和↓/s实现视角水平旋转  
	        if (Input.GetAxis("Horizontal") != 0)
	        {
	            transform.Translate(Input.GetAxis("Horizontal") * sensitivetyKeyBoard, 0, 0);
	        }
	        if (Input.GetAxis("Vertical") != 0)
	        {
	            transform.Translate(0, Input.GetAxis("Vertical") * sensitivetyKeyBoard, 0);
	        }
	    }
	
	}
	```
* 实现效果
见[我的GitHub](https://github.com/Shanzhulizhi/3dGame-hw2)

### 编程实践
**【阅读以下游戏脚本】**

*Priests and Devils*

*Priests and Devils is a puzzle game in which you will help the Priests and Devils to cross the river within the time limit. There are 3 priests and 3 devils at one side of the river. They all want to get to the other side of this river, but there is only one boat and this boat can only carry two persons each time. And there must be one person steering the boat from one side to the other side. In the flash game, you can click on them to move them and click the go button to move the boat to the other direction. If the priests are out numbered by the devils on either side of the river, they get killed and the game is over. You can try it in many  ways. Keep all priests alive! Good luck!*

程序需要满足的要求：
 + play the game ( http://www.flash-game.net/game/2535/priests-and-devils.html )
+ 列出游戏中提及的事物（Objects）
+ 用表格列出玩家动作表（规则表），注意，动作越少越好
+ 请将游戏中对象做成预制
+ 在 GenGameObjects 中创建 长方形、正方形、球 及其色彩代表游戏中的对象。
+ 使用 C# 集合类型 有效组织对象
+ 整个游戏仅 主摄像机 和 一个 Empty 对象， 其他对象必须代码动态生成！！！ 。 整个游戏不许出现 Find 游戏对象， SendMessage 这类突破程序结构的 通讯耦合 语句。 违背本条准则，不给分
+ 请使用课件架构图编程，不接受非 MVC 结构程序
+ 注意细节，例如：船未靠岸，牧师与魔鬼上下船运动中，均不能接受用户事件！

**【游戏中提及的对象】**
牧师，魔鬼，小船，小河，河岸
**【玩家动作表】**
动作 | 条件 | 结果
-------- | ----- | ---
点击岸上的人 | 岸上有人，船上有空位| 人进入船，岸上人数减少
点击船|船上有人|船移动到对岸
点击船上的人|船上有人|人上岸，船上人数减少
**【MVC架构图】**
![在这里插入图片描述](https://img-blog.csdnimg.cn/20190920210444353.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3h1YW5fdGluZw==,size_16,color_FFFFFF,t_70)

**【实现】**
___
* **Director**
Director是最高层的控制器，运行游戏时始终只有一个实例，它掌控着场景的加载、切换等，也可以控制游戏暂停、结束等等。
```
public class SSDirector : System.Object {
    private static SSDirector Myinstance;
    public SceneController FirstController { get; set; }

    public static SSDirector getInstance() {
        if (Myinstance == null) {
            Myinstance = new Director ();
        }
        return Myinstance;
    }
}
```
* **SceneController接口**
SceneController 是导演控制场景控制器的渠道。在上面的	SSDirector 类中，FirstController 就是SceneController的实现。
```
public interface SceneController {
    void loadResources ();
}
```
* **Moveable类**
挂载在GameOject上实现游戏对象的运动。
* **CharacterController类**
对游戏角色进行封装，在构造函数中动态实例化了一个perfab，创建GameObject。另外实现了对游戏角色状态和行为的控制函数。

(1)实例化预制对象(以Priest为例)
```
character = Object.Instantiate(Resources.Load("Priest", typeof(GameObject)), Vector3.zero, Quaternion.identity, null) as GameObject;
```
 (2)角色状态和行为设置
```
public void setname(string name)  //设置对象名称
public void setPosition(Vector3 pos)  //设置对象位置
public void moveToPosition(Vector3 dest)  //设置对象移动
public int getType()  //返回对象角色
public string getName()  //返回对象名称
public void getOnBoat()  //上船
public void getOnCoast()  //上岸
public bool isOnBoat()  //判断是否在船上
```
* **BoatController类**
封装了船类，提供getEmptyPosition()方法，给出自己的空位，让游戏角色能够移动到合适的位置。

(1)预制船和设置起始状态
```
 from_positions = new Vector3[] { new Vector3(4.5F, 1.5F, 0), new Vector3(5.5F, 1.5F, 0) };
        to_positions = new Vector3[] { new Vector3(-5.5F, 1.5F, 0), new Vector3(-4.5F, 1.5F, 0) };

        boat = Object.Instantiate(Resources.Load("Boat", typeof(GameObject)), fromPosition, Quaternion.identity, null) as GameObject;
        boat.name = "boat";
 ```
 (2)设置船的行为
 ```
 public void Move()  //船的移动
 public  Vector3 getEmptyPosition()   //得到船上的孔伟
 public bool isEmpty()  //判断船是否为空
 public void GetOnBoat(CharacterController chCtrl)  //调用船的上船状态
 ```
 * **CoastController类**
 封装了河岸，具体内容和BoatController类相同。
 * **UserAction接口**
 FirstController必须要实现这个接口才能对用户的输入做出反应。在ClickGUI和UserGUI这两个类中，都保存了一个UserAction的引用。当ClickGUI监测到用户点击GameObject的时候，就会调用这个引用的characterIsClicked方法，这样FirstController就知道哪一个游戏角色被点击了。UserGUI同理，它监测的是“用户点击Restart按钮”的事件。
 ```
 public interface UserAction {
    void moveBoat();
    void characterIsClicked(CharacterController characterCtrl);
    void restart();
}
 ```
 * **ClickGUI类**
 lickGUI类是用来监测用户点击，并调用SceneController进行响应的。
 ```
 public class ClickGUI : MonoBehaviour {
    UserAction action;
    CharacterController characterController;

    public void setController(CharacterController characterCtrl) {
        characterController = characterCtrl;
    }

    void Start() {
        action = Director.getInstance ().FirstController as UserAction;
    }

    void OnMouseDown() {
        if (gameObject.name == "boat") {
            action.moveBoat ();
        } else {
            action.characterIsClicked (characterController);
        }
    }
}
 ```
 ---
**【游戏展示视频】**
见[我的Github](https://github.com/Shanzhulizhi/3dGame-hw2)
### 【参考博客】
https://blog.csdn.net/yaoxh6/article/details/79773632

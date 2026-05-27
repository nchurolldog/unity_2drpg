# 2D RPG 学习开发计划 (Unity 6 + Tiny Swords)

## 素材分析摘要
- **角色**: Warrior(1152×1536, 3×4帧spritesheet), Pawn(1152×1152, 3×3帧), Archer(单图+手臂分离) — 四种颜色变体
- **地形**: Ground(Tilemap_Flat 640×256 + Tilemap_Elevation 256×512), Water, Bridge
- **建筑**: Castle, Tower, House (颜色变体 + 建造/摧毁状态)
- **资源**: Gold Mine (Active/Inactive/Destroyed), Trees, Sheep
- **特效**: Explosions.png(1728×192横条), Fire.png(896×128横条)
- **UI**: Banners, Buttons, Icons, Pointers, Ribbons
- **已配置**: InputSystem (Player + UI map), SampleScene

---

## Phase 1 — 项目架构与 Tilemap 地图

**目标**: 搭建 Unity 2D 项目基础，学会像素素材导入配置与 Tilemap 地图构建。

### 步骤
1. **Sprite 导入配置**
   - 设置 Pixels Per Unit (推荐 16 或 32)
   - 切割 Warrior spritesheet: 3×4 网格 (每格 384×384)
   - 切割 Pawn, 地面 Tile, 特效 strip
   - Filter Mode → Point (no filter) 保持像素风格
   - Compression → None

2. **Tilemap 地图构建**
   - 创建 Grid → 多个 Tilemap 层:
     - `Ground` 层 (Tilemap_Flat)
     - `Elevation` 层 (Tilemap_Elevation, 用于高处/装饰)
     - `Decoration` 层 (花草树木等)
     - `Collision` 层 (不可行走区域, 用 TilemapCollider2D)
   - 使用 Tile Palette 工具手动绘制一个测试地图

3. **项目目录结构**
   ```
   Assets/
   ├── rpg/Tiny Swords/          (原始素材, 只读)
   ├── Art/                       (切割后的精灵引用)
   ├── Animations/                (动画控制器/片段)
   ├── Scripts/
   │   ├── Player/
   │   ├── Enemies/
   │   ├── Combat/
   │   ├── UI/
   │   └── Core/
   ├── Prefabs/
   ├── Scenes/
   │   ├── MainMenu.unity
   │   ├── Level_01.unity
   │   └── ...
   └── ScriptableObjects/         (敌人数据/物品数据等)
   ```

### 学习要点
- Sprite Renderer, Sprite Editor 的多图切割
- Tilemap, Tile Palette, Grid 组件
- Sorting Layer 排序层级管理
- TilemapCollider2D + CompositeCollider2D 的地形碰撞

---

## Phase 2 — 角色核心: 移动与动画

**目标**: 实现平滑 8 方向移动与 Blend Tree 动画控制。

### 步骤
1. **Player GameObject 组装**
   - Rigidbody2D (Dynamic, Gravity=0)
   - BoxCollider2D / CapsuleCollider2D
   - SpriteRenderer + Animator

2. **输入读取** — 复用已有的 `InputSystem_Actions`
   - 通过 C# 事件订阅 `Move` action (WASD/手柄)
   - `Attack`, `Interact` 按钮

3. **PlayerController 脚本**
   - `Scripts/Player/PlayerController.cs`
   - 8 方向移动 (Vector2 input → Rigidbody2D.velocity)
   - 根据移动方向更新 Animator 参数 (MoveX, MoveY, Speed)

4. **Blend Tree 动画设置**
   - Animator Controller → Blend Tree (2D Freeform Directional)
   - 4 方向 idle (上/下/左/右) + 4 方向 walk
   - 使用 Warrior spritesheet 的切片制作 Animation Clip

### 学习要点
- New Input System 的 C# 事件订阅模式
- Rigidbody2D 运动 (velocity vs MovePosition)
- Animator Controller 与 Blend Tree
- 2D Freeform Directional 参数配置

---

## Phase 3 — 战斗系统

**目标**: 近战挥砍与远程投射物，精准攻击判定与受击反馈。

### 步骤
1. **近战攻击 (Melee)**
   - `Scripts/Combat/MeleeAttack.cs`
   - 攻击动画 + Attack point (子 GameObject 带 Collider2D 作为 hitbox)
   - 攻击时启用 hitbox，动画事件控制启用/禁用时机
   - 检测碰撞→造成伤害

2. **远程攻击 (Ranged)**
   - `Scripts/Combat/Projectile.cs`
   - 实例化 Arrow prefab，沿方向飞行
   - Rigidbody2D.velocity 驱动
   - 碰撞到敌人→造成伤害+销毁

3. **受击反馈**
   - `Scripts/Combat/HitFeedback.cs`
   - 收到伤害时: SpriteRenderer 闪白 (coroutine)
   - 短暂击退 (Rigidbody2D.AddForce)

### 学习要点
- Animation Event 的使用
- Collider2D 作为 hitbox 的启用/禁用时机
- Prefab 实例化 (Instantiate)
- Coroutine 实现受击闪白效果

---

## Phase 4 — 敌人 AI

**目标**: 基于距离感知的智能 AI，巡逻→追击→攻击行为链。

### 步骤
1. **敌人基础组件**
   - `Scripts/Enemies/EnemyAI.cs` — 状态机 (Idle / Patrol / Chase / Attack)
   - 复用 Phase 2 的移动逻辑

2. **行为逻辑**
   - **Idle**: 原地停留 N 秒
   - **Patrol**: 随机方向移动 / Waypoint 巡逻
   - **Chase**: 检测到玩家进入追击范围 → 向玩家移动
   - **Attack**: 进入攻击范围 → 执行近战/远程攻击

3. **距离检测**
   - `Physics2D.OverlapCircle` 或简单的 `Vector2.Distance` 检测
   - 在 Inspector 中可配置: patrolRange, chaseRange, attackRange

4. **Prefab 制作**
   - Enemy 基础 Prefab (带 EnemyAI, Health, Rigidbody2D)
   - 为每种敌人变体 (颜色) 创建变体 Prefab

### 学习要点
- 简单状态机模式 (enum + switch)
- Physics2D.OverlapCircle 用于范围检测
- Prefab Variants 的使用

---

## Phase 5 — 属性管理

**目标**: 模块化生命值系统，涵盖伤害计算、击退与死亡。

### 步骤
1. **Health 组件**
   - `Scripts/Core/Health.cs`
   - `maxHealth`, `currentHealth`, `TakeDamage(float amount, Vector2 knockbackDir)`
   - UnityEvent `OnDamaged`, `OnDeath`, `OnHealed`
   - `IsAlive` 属性

2. **伤害接口**
   - `Scripts/Core/IDamageable.cs`
   - `void TakeDamage(float amount, Vector2 knockbackDirection)`

3. **死亡逻辑**
   - 播放死亡动画 / 切换死亡精灵
   - 禁用 Collider + AI
   - 延迟销毁或对象池回收

4. **ScriptableObject 数据**
   - `EnemyData.asset` — 存储血量、速度、攻击力等
   - `WeaponData.asset` — 武器伤害、攻击范围等

### 学习要点
- UnityEvent 的事件驱动架构
- C# Interface 的设计与使用
- ScriptableObject 用作数据容器

---

## Phase 6 — UI 与特效

**目标**: 动态血条、伤害飘字、屏幕震动、粒子特效。

### 步骤
1. **世界空间血条**
   - `Scripts/UI/HealthBarUI.cs`
   - Canvas (World Space) + Slider
   - 跟随敌人/玩家头顶，根据血量百分比更新

2. **伤害飘字 (Damage Popup)**
   - `Scripts/UI/DamagePopup.cs`
   - TextMeshPro 文本，向上漂浮 + 淡出
   - 对象池管理 (避免频繁 Instantiate/Destroy)

3. **屏幕震动 (Screen Shake)**
   - `Scripts/Core/ScreenShake.cs`
   - 受击时触发 Camera offset 的 coroutine
   - 可配置: 强度 (amplitude) + 持续时间 (duration)

4. **粒子特效**
   - 利用 Explosions/Fire 素材制作粒子动画
   - 攻击命中时生成爆炸粒子
   - 可破坏物销毁时生成碎片效果

### 学习要点
- Canvas 的 World Space 渲染模式
- TextMeshPro 的基本使用
- 简单对象池模式 (Queue<GameObject>)
- Cinemachine 或手动 Camera shake

---

## Phase 7 — 交互系统

**目标**: 可破坏物体、场景交互元素。

### 步骤
1. **可破坏物**
   - `Scripts/Core/Destructible.cs`
   - 实现 IDamageable
   - 受击→销毁→生成粒子→可能掉落物品

2. **场景交互**
   - `Scripts/Core/Interactable.cs` (基类)
   - 宝箱 (Chest): 靠近按 E→播放打开动画→获得物品
   - 标识 (Sign): 靠近按 E→显示文字
   - 门/传送点: 靠近按 E→切换场景

3. **交互检测**
   - `Physics2D.OverlapCircle` 检测交互范围内的 Interactable
   - UI 提示 (按键图标浮现在可交互物上方)

### 学习要点
- 抽象基类 + 继承多态
- 交互范围检测
- 简单对话/提示 UI

---

## Phase 8 — 场景管理与数据持久化

**目标**: 场景切换、相机跟随、基础存档。

### 步骤
1. **相机跟随**
   - 使用 Cinemachine 2D Camera
   - Follow 绑定 Player Transform
   - 配置 Confiner (限制相机在地图边界内)

2. **场景切换**
   - `Scripts/Core/SceneLoader.cs`
   - 异步加载 (LoadSceneAsync)
   - 过渡效果 (淡入淡出)

3. **数据持久化**
   - `Scripts/Core/SaveManager.cs`
   - 使用 `Application.persistentDataPath` + JSON
   - 保存: 玩家位置、血量、已击败敌人、已打开宝箱
   - PlayerPrefs 用于设置项 (音量等)

4. **主菜单场景**
   - 开始游戏 / 继续游戏 / 设置 / 退出
   - SceneLoader 加载对应场景

### 学习要点
- Cinemachine Virtual Camera 配置
- 异步场景加载与加载画面
- JSON 序列化/反序列化 (JsonUtility)
- Application.persistentDataPath 跨平台路径

---

## 关键架构决策

| 决策 | 选择 | 理由 |
|------|------|------|
| 移动方式 | Rigidbody2D.velocity | 物理驱动，自带碰撞处理 |
| 动画系统 | Animator + Blend Tree | Unity 原生 |
| 碰撞检测 | 2D Physics (Collider2D) | 精确易用 |
| 数据配置 | ScriptableObject | Inspector 友好 |
| 存档格式 | JSON + persistentDataPath | 跨平台，可读 |
| 输入 | New Input System | 已配置，多设备支持 |
| 状态管理 | enum + switch 状态机 | 简单直观 |

## 学习路线建议
- Phases 1-2 是基础，完成后就能让角色在地图上跑起来
- Phase 3-4 让游戏有"打斗"感，是核心玩法
- Phase 5-6 是打磨体验，伤害数字/屏幕震动等大幅提升手感
- Phase 7-8 是完整游戏所需的周边系统
- 每个 Phase 的脚本都尽量独立，方便逐步理解和调试

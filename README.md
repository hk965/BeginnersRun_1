# 【ゲーム紹介】
## 　●コンセプト
　　〇初心者向け横スク2Dゲーム  
　　〇最速でゴールを目指すタイムアタックゲーム
## 　●概要
　　〇ルールがシンプル → とにかく早くゴールを目指す横スクロールのゲーム  
　　〇これだけではちょっと味気ないので… → 障害物や敵の攻撃、自分に有利なスキルを実装  
　　〇操作方法が簡単 → 複雑なコマンドは無し！左右の移動、ジャンプ、スキルのみ  

## 　●操作方法（PC操作）  
　　〇左右移動："←"キー、および、"→"キー  
　　〇ジャンプ："space"キー  
　　〇スキル："s"キー  

# 【デモ動画】
## 　●プレイ動画
　　[1ステージ分のプレイ動画](https://drive.google.com/file/d/1fkCAJ4GZM6Wj7nUZNFv5Cg1vj-h1u7yT/view?usp=drive_link)   

## 　●移動とジャンプ
　　[移動とジャンプの挙動](https://drive.google.com/file/d/16UcsK4wjxcOeELfYkxxCovaW5E4gPBwG/view?usp=drive_link) ：  
　　ジャンプボタンを押す長さによって、ジャンプ力が変化する  
　　ジャンプには上昇限界がある   

## 　●障害物に当たったとき（プレイヤーが減速する）
　　[トゲ床](https://drive.google.com/file/d/13W3cmqwd47rSOrI6QJ9z2ouU52E5K1XF/view?usp=drive_link)    
　　[遠距離攻撃するエネミー](https://drive.google.com/file/d/1DPpBD1YuofpD3ooQnFvnyvCm-25nwSXR/view?usp=drive_link) ：プレイヤーのいた場所に弾を発射  

## 　●特殊な足場（足場の下から足場に乗ることができる）
　　[特殊な足場1](https://drive.google.com/file/d/1aBFqoC-6PtVDRzxMC-6boVYqzy1smOs_/view?usp=drive_link) ：止まった足場と上下に動く床  
　　[特殊な足場2](https://drive.google.com/file/d/1mzeKx1jzRggudxlKmxZxVlImPPt_Wohr/view?usp=drive_link) ：横に動く足場  
　　[特殊な足場3](https://drive.google.com/file/d/1FJUJGzwJjL0HsVAPO-rBsx10PRH7FT7A/view?usp=drive_link) ：斜めに動く足場  

## 　●スキル1：加速
　　[10秒間、左右方向に加速](https://drive.google.com/file/d/19pE7iPurC7OxMVaootDYQYWuWQ_Vu0GW/view?usp=drive_link)   

## 　●スキル2：2段ジャンプ
　　[10秒間、空中で1度だけジャンプ可能](https://drive.google.com/file/d/14ismaB7et72Qz1wqX91jBKHqbNXnene1/view?usp=drive_link)  

## 　●スキル3：無敵
　　[10秒間、無敵](https://drive.google.com/file/d/1lIpqbhr1hLr9RDLXClV8znOUlpRS0OVu/view?usp=drive_link) ：減速中に発動すると、元の速度に戻せる  

# 【工夫した点】
## 　●"Start"シーン
　　〇このシーンでは、キャラクターが永遠に右に走り続ける。しかし、地面の長さには限りがある。  
　　　そこで、特定の位置にキャラクターが到達した時にキャラクターの位置を左へ戻すことで、  
　　　短い地面を用意するだけでも常に右に進み続けているように見せることができた。  
　　　【該当箇所】  
　　　StartScript：30～34行目  
  
## 　●"Skill"シーン  
　　〇スキルの数が増えた時のために、スキルボタンは配列を用いて管理できるようにした。  
　　　これにより、Unityのエディターにおいて、"SkillManager"オブジェクトのInspectorから  
　　　容易にスキルを増やせる。  
　　　【該当箇所】  
　　　SkillScript：11行目  
     
　　　また、スキルボタンを押すと、ボタンに割り振られた番号を返すようにすることで、  
　　　スキルボタン選択時にボタンの色が「黄→赤」に変わる挙動を容易に実装できた。  
　　　【該当箇所】  
　　　SkillScript：60～65行目  
     
　　〇スキル未選択の状態で次のシーンに移行することがないように、  
　　　一度でもスキルボタンが押されなければ決定ボタンを押せないようにした。  
　　　【該当箇所】  
　　　SkillScript：23～24行目（スキルに割り当てた番号の消去と決定ボタン選択不可）  
　　　SkillScript：34～37行目（スキルを選択したら決定ボタンが押せるようになる）  

## 　●"Stage"シーン  
　　〇"Skill"シーンと同様、ステージは配列を用いて管理。  
　　　また、ステージボタンを押すと、ボタンに割り振られた番号を返し、ボタンの色変更を行う。  
　　　【該当箇所】  
　　　StageScript：11行目（配列の用意）  
　　　StageScript：60～65行目（ボタンの色の挙動）  

　　〇"Skill"シーンと同様、ステージ未選択で次のシーンへ移行しないよう、  
　　　一度でもステージボタンが押されなければ決定ボタンを押せないようにした。  
　　　【該当箇所】  
　　　StageScript：23～24行目（ステージに割り当てた番号の消去と決定ボタン選択不可）  
　　　StageScript：34～37行目（ステージを選択したら決定ボタンが押せるようになる）  

## 　●"Main"シーン  
　　〇選択したステージの開始位置にキャラクターが来るように、  
　　　"Stage"シーンにて取得したステージ番号を活用。  
　　　番号に応じて開始位置を指定するようにした。  
　　　【該当箇所】  
　　　MainScript：95～103行目  
     
　　〇"Skill"シーンにて取得したスキルの番号を基に、選択したスキルを"Main"シーン内のSKILLボタンに反映。  
　　　【該当箇所】  
　　　MainScript：123～124行目  
     
　　　なお、124行目のコードはwebサイトで見つけたコードを参考にしている（【参考にした箇所】を参照）  
　　　が、スキルが増える度にif文で反映させるのは手間だと感じ、1行で反映の工程が完了するように  
　　　工夫をした。  
　　　具体的には、SkillMaker関数へ引数としてスキル番号を渡すことで、  
　　　SkillMaker関数内で番号に応じた処理（スキル）が実行されるようにした。  
　　　これにより、スキルが増えた際はSkillMaker関数のみを編集すれば、  
　　　求めるスキルが自動的にSKILLボタンに紐づけられる。  
　　　【該当箇所】  
　　　MainScript：299～319行目  
  
　　〇ジャンプ時に、ボタンを押す時間によってジャンプ力を任意に決めることができるようにするため、  
　　　フラグ"pushjump"を用意し、JUMPボタンを押している間はtrue、離すとfalseにすることで、  
　　　"pushjump"がtrueで上昇、falseで下降するように実装した。  
　　　また、JUMPボタンを押し続けることで上昇し続けてしまうのを防ぐために、高度制限と滞空時間を設けた。  
　　　【該当箇所】  
　　　MainScript：328～339行目（上昇開始時の挙動）  
　　　MainScript：342～356行目（上昇中の挙動）  
　　　MainScript：491～494行目（落下時の挙動。"isGround"は接地判定）  
     
　　　そして、キャラクターが天井に頭をぶつけた際には天井に向かってぶつかり続けるのを防ぐため、  
　　　頭の当たり判定"isHead"を用い、天井とぶつかった（"isHead"がtrueの）ときに  
　　　強制的に落下を開始するようにした。  
　　　【該当箇所】  
　　　MainScript：377～383行目  
  
　　〇遠距離攻撃するエネミーは、一定範囲内にいるキャラクターに向けて、一定間隔で弾を発射する。  
　　　常にキャラクターとの距離を測り、一定距離以内になったときに弾をエネミー自身の位置に生成する。  
　　　【該当箇所】  
　　　AttackerScript：30～48行目  
  
　　　生成された弾は、キャラクターの位置に向けて移動する。  
　　　ただし、処理が重くなってしまうのを防ぐため、弾とキャラクターとの距離が  
　　　一定以上離れると弾は消滅する。  
　　　【該当箇所】  
　　　AttackScript：31～53行目  

　　〇スキル1（加速）発動時は、現在のスピード（変数"speed"）に直接加速の倍率を掛けるのではなく、  
　　　キャラクターの元のスピード（定数"originalspeed"）に掛けることにより、万が一、  
　　　加速処理が重複して実行されてしまっても速度が上がり続けてしまわないようにしている。  
　　　【該当箇所】  
　　　MainScript：303行目  

　　　また、フラグ"speedUp"を用いて、被ダメージ時の減速処理を加速時と非加速時に分けている。  
　　　【該当箇所】  
　　　MainScript：618～630行目    

　　　そして、減速の解除時に未だスキル発動中であった場合は、減速解除後の速度が  
　　　加速時の速度に戻るように、減速解除の処理を加速時と非加速時に分けている。  
　　　【該当箇所】  
　　　MainScript：654～662行目    
    
　　〇スキル2（2段ジャンプ）発動時は、フラグ"secondjump"をtrueにしつつ、  
　　　ジャンプの回数を変数"jumpCount"にてカウントし、これらを条件に組み込んだ条件分岐により、  
　　　空中でもう1度だけジャンプできるように制御を記述した。  
　　　【該当箇所】  
　　　MainScript：359～375行目   

　　〇スキル3（無敵）発動時に減速中だった場合、減速時の処理を取り消す。  
　　　【該当箇所】  
　　　MainScript：314～317行目   

　　　また、フラグ"muteki"を用い、トゲ床やエネミーによる遠距離攻撃（弾）に当たった際に  
　　　スキル発動中である場合、減速処理が行われないようにしている。  
　　　【該当箇所】  
　　　MainScript：556～567行目（トゲ床に当たった際の挙動）   
　　　MainScript：543～550行目（トゲ床に当たった際の挙動）   
　　　MainScript：574～586行目（弾に当たった際の挙動）   
　　　なお、この箇所において、スキル発動時はフラグ"slower"をfalseにすることで、  
　　　無敵中に不要な「減速解除の処理」を省いている。  
　　　【該当箇所】  
　　　MainScript：214～217行目   

  
# 【webサイトを参考にした箇所】  
　　〇MainScript：124行目（スキル番号を基にSKILLボタンに関数を割り当てる処理）  
　　　[Unity – UIのButtonにイベントを設定する方法まとめ](https://santerabyte.com/unity-ui-button-add-click-event/#%E3%82%B9%E3%82%AF%E3%83%AA%E3%83%97%E3%83%88%E3%81%8B%E3%82%89%E8%BF%BD%E5%8A%A0%E3%81%99%E3%82%8B)  
  
　　〇MoveObject：スクリプト全体（移動する床の動きを記述したスクリプト）  
　　　[Unity動く床・落ちる床・下からのみすり抜ける床の作り方【2Dアクション】](https://dkrevel.com/makegame-beginner/make-2d-action-move-and-fall-floor/#%E5%8B%95%E3%81%8F%E5%BA%8A%E3%81%AE%E3%82%B9%E3%82%AF%E3%83%AA%E3%83%97%E3%83%88%E8%A7%A3%E8%AA%AC)  

　　〇GroundChecker：7, 44～48, 58～62, 72～76行目（下から乗ることができる床の当たり判定）  
　　　[Unity動く床・落ちる床・下からのみすり抜ける床の作り方【2Dアクション】](https://dkrevel.com/makegame-beginner/make-2d-action-move-and-fall-floor/#%E4%B8%8B%E3%81%8B%E3%82%89%E3%81%AE%E3%81%BF%E3%81%99%E3%82%8A%E6%8A%9C%E3%81%91%E3%82%8B%E5%BA%8A%E3%81%AE%E4%BD%9C%E3%82%8A%E6%96%B9)  
   
　　〇MainScript：511～515行目（動く床で自然にキャラクターが動くようになる処理）  
　　　[Unity動く床・落ちる床・下からのみすり抜ける床の作り方【2Dアクション】](https://dkrevel.com/makegame-beginner/make-2d-action-move-and-fall-floor/#%E5%8B%95%E3%81%8F%E5%BA%8A%E3%81%A7%E6%BB%91%E3%82%89%E3%81%AA%E3%81%84%E3%82%88%E3%81%86%E3%81%AB%E3%81%99%E3%82%8B)  
   

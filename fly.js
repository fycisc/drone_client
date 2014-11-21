var play:GameObject;
function Update () {
}
function OnGUI(){
if(GUI.Button(Rect(215,280,75,30),"Normal-Walk")){
    play.GetComponent("FPSWalker").enabled  = false;
    play.GetComponent("testFly").enabled  = true;
}
if(GUI.Button(Rect(230,200,50,30),"Start-Fly")){
    play.GetComponent("FPSWalker").enabled  = true;
    play.GetComponent("testFly").enabled  = false;
}
}
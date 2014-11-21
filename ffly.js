var sensitivity : float = .01; 
var moveSpeed : float = 100f;
function Update() { 
   //transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity);   
   transform.Translate(Vector3.forward * Time.deltaTime*moveSpeed );   // 
//   transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime* moveSpeed); // 
   //transform.Translate(Vector3.up * Input.GetAxis("UpDown") * Time.deltaTime* moveSpeed);    
   if (Input.GetKey(KeyCode.Space))   transform.Translate(Vector3.up * Time.deltaTime* moveSpeed); 
   if (Input.GetKey(KeyCode.LeftShift )&&transform.position.y>=1.9)   transform.Translate(Vector3.down * Time.deltaTime* moveSpeed); 
   if (Input.GetKey(KeyCode.E))   {
   				transform.RotateAround(transform.position,Vector3.up, 40*moveSpeed*Time.deltaTime * sensitivity);
//   				transform.Translate(Vector3.forward * Time.deltaTime*moveSpeed );   // 
   				}   
      if (Input.GetKey(KeyCode.Q))   {
      			transform.RotateAround(transform.position,Vector3.up, -40*moveSpeed*Time.deltaTime * sensitivity);   
//      			transform.Translate(Vector3.forward * Time.deltaTime*moveSpeed );   // 
      			}
      			
      			


}
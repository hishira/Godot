shader_type canvas_item;

uniform bool active = false; // like export variable, global variable in GLS

// Run on evry single pixel
void fragment() {
	vec4 previous_color = texture(TEXTURE, UV);
	vec4 white_color = vec4(1.0, 1.0, 1.0, previous_color.a); // a - represent alpha
	vec4 new_color = previous_color;
	if(active){
		new_color = white_color;
	}
	COLOR = new_color; // red, green, blue, alpha 
}
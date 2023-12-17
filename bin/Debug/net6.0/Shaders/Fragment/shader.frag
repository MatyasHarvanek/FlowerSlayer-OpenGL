#version 330 core
out vec4 FragColor;

in vec3 O_color;

void main()
{
    FragColor = vec4(O_color, 1.0f);
}
#version 330 core
layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec3 color;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

out vec3 O_color;

void main()
{
    O_color = color;
    gl_Position = vec4(aPosition, 1.0) * model * view *projection;
}
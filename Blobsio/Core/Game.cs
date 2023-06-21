﻿global using Time = Blobsio.Core.Time;
using Blobsio.Recources;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Numerics;
using System.Reflection;

namespace Blobsio.Core;

public class Game
{
    private RenderWindow window;

    private Renderer renderer = new Renderer();
    private Physics physics = new Physics();
    private Input input = new Input();

    public const int MAP_SIZE = 5000;

    private List<Entity> entities = new List<Entity>();
    private List<Entity> newEntities = new List<Entity>();
    private List<Entity> pendingEntitiesToDestroy = new List<Entity>(); 

    private Game() { }

    public void Run()
    {
        Time.Start();
        renderer.Start(window);
        input.Start(window);

        InitializeEntities();

        while (window.IsOpen)
        {
            Time.Update();

            physics.Update(entities.ToArray());

            input.Update();
            Start();
            Update();
            LateUpdate();
            Destroy();

            renderer.Render(entities.ToArray());
        }
    }

    private void Start()
    {
        if (newEntities.Count == 0)
            return;
        
        for (int i = 0; i < newEntities.Count; i++)
        {
            newEntities[i].Start();
        }

        entities.AddRange(newEntities);
        newEntities.Clear();
    }

    private void Update()
    {
        for (int i = 0; i < entities.Count; i++)
        {
            entities[i].Update();
        }
    }

    private void LateUpdate()
    {
        for (int i = 0; i < entities.Count; i++)
        {
            entities[i].LateUpdate();
        }
    }

    private void Destroy()
    {
        for (int i = 0; i < entities.Count; i++)
        {
            entities[i].Destroy();
        }

        if (pendingEntitiesToDestroy.Count == 0)
            return;
        
        for (int i = 0; i < pendingEntitiesToDestroy.Count; i++)
        {
            entities.Remove(pendingEntitiesToDestroy[i]);
        }

        pendingEntitiesToDestroy.Clear();
    }

    private void InitializeEntities()
    {
        for (int i = 0; i < entities.Count; i++)
        {
            entities[i].OnInstantiate(this);
            entities[i].Start();
        }
    }

    public Entity FindEntitiyByTag(string tag)
        => entities.Find(x => x.tag == tag);

    public Entity[] FindEntitiesByTag(string tag)
        => entities.FindAll(x => x.tag == tag).ToArray();

    public Entity Instantiate(Entity e)
    {
        newEntities.Add(e);

        e.OnInstantiate(this);

        return e;
    }

    public Entity Instantiate(Entity e, Vector2f position)
    {
        newEntities.Add(e);

        e.OnInstantiate(this);
        e.position = position;

        return e;
    }

    public void Destroy(Entity e)
    {
        pendingEntitiesToDestroy.Add(e);
    }


    public static Game Create(List<Entity> entities)
    {
        Game game = new Game();
        game.entities = entities;
        game.LoadCongig();

        return game;
    }

    private void LoadCongig() // xd
    {
        StreamReader reader = new StreamReader(RecourcesManager.GetConfig("config"));

        while (!reader.EndOfStream)
        {
            string[] input = reader.ReadLine().Split(':');

            if (input.Length < 2)
                continue;

            string name = input[0];
            string[] values = input[1].Split(',');

            if (values.Length < 0)
                continue;

            FieldInfo field = GetType().GetField(name, BindingFlags.NonPublic | BindingFlags.Instance);

            if (field == null)
                continue;

            switch (field.FieldType.Name)
            {
                case "Int32": // ЧОГО МЕНІ ПРОСТО НЕ ДАДУТЬ НОРМАЛЬНИЙ ТИП А НЕ ЯКЕСЬ ЛАЙНО
                    if (int.TryParse(input[1], out int value))
                    {
                        GetType().GetField(name, BindingFlags.NonPublic | BindingFlags.Instance)?
                            .SetValue(this, value);
                    }
                    break;

                case "RenderWindow":
                    if (uint.TryParse(values[0], out uint value1) && uint.TryParse(values[0], out uint value2))
                    {
                        GetType().GetField(name, BindingFlags.NonPublic | BindingFlags.Instance)?
                            .SetValue(this, new RenderWindow(new VideoMode(value1, value2), values[2]));
                    }
                    break;
            }
        }

        reader.Close();
    }
}

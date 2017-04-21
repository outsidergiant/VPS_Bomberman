using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedIntelligentSystem : IntelligentSystemBase {

    private List<Vector3> path;
    private List<Vector3> tempPath;
    private int counter = 0;

    public AdvancedIntelligentSystem(Enemy character) : base(character)
    {
        this.character = character;
        path = new List<Vector3>();
        tempPath = new List<Vector3>();
    }

    public override List<Vector3> CalculatePath()
    {
        InitFieldMap();
        return ChooseOptimalDirection();
    }

    private void InitFieldMap()
    {
        FillTheDictionaryByDefault();
        UpdateBrickBarriers();
    }

    private void FillTheDictionaryByDefault()
    {
        character.fieldMap.Clear();
        for (int x = 0; x < GlobalParameters.numberTilesX; x++)
        {
            for (int z = 0; z < GlobalParameters.numberTilesZ; z++)
            {
                character.fieldMap.Add(new Vector3(x, 0f, z), int.MaxValue);
            }
        }
    }

    private void BlockTheWayForObjectWithTag(string tag)
    {
        GameObject[] barrier = GameObject.FindGameObjectsWithTag(tag);
        for (int i = 0; i < barrier.Length; i++)
        {
            character.fieldMap[barrier[i].transform.position] = -1;
        }
    }

    private void UpdateBrickBarriers()
    {
        BlockTheWayForObjectWithTag("Concrete");
        if (!character.abilities.Contains(PowerUp.Wallpass))
        {
            BlockTheWayForObjectWithTag("Brick");
        }
    }

    public virtual List<Vector3> ChooseOptimalDirection()
    {
        Vector3 currentPosition = character.gameObject.transform.position;
        currentPosition.x = Mathf.RoundToInt(currentPosition.x);
        currentPosition.y = 0f;
        currentPosition.z = Mathf.RoundToInt(currentPosition.z);
        Vector3 end = GameObject.FindGameObjectWithTag("Player").transform.position;
        end.x = Mathf.RoundToInt(end.x);
        end.z = Mathf.RoundToInt(end.z);
        character.fieldMap[currentPosition] = 0;
        end = CorrectEndPointIfPlayerIsInTheWall(end, character.fieldMap);
        tempPath = new List<Vector3>();
        tempPath.Add(currentPosition);
        UpdateFieldMap(currentPosition, end, character.fieldMap);
        path = FindPathFromEnd(end, character.fieldMap);
        if (path != null)
        {
            path.RemoveAt(path.Count - 1);
        }
        return path;
    }

    protected virtual Vector3 CorrectEndPointIfPlayerIsInTheWall(Vector3 end, Dictionary<Vector3, int> map)
    {
        int value;
        map.TryGetValue(end, out value);
        if (value == -1)
        {
            do
            {
                if (CorrectEndPoint(map, end, Vector3.forward, out end))
                {
                    break;
                }
                else if (CorrectEndPoint(map, end, Vector3.back, out end))
                {
                    break;
                }
                else if (CorrectEndPoint(map, end, Vector3.left, out end))
                {
                    break;
                }
                else if (CorrectEndPoint(map, end, Vector3.right, out end))
                {
                    break;
                }

            } while (map[end] == -1);
        }
        return end;
    }

    protected virtual bool CorrectEndPoint(Dictionary<Vector3, int> map, Vector3 tempEnd, Vector3 direction, out Vector3 end)
    {
        if (map[tempEnd + direction] != -1)
        {
            end = tempEnd + direction;
            return true;
        }
        end = tempEnd;
        return false;
    }

    protected virtual void UpdateFieldMap(Vector3 currentPosition, Vector3 end, Dictionary<Vector3, int> map)
    {
        counter = 0;
        List<Vector3> directions = new List<Vector3>() { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };
        while (counter != 4)
        {
            Vector3 oldCurrentPosition = currentPosition;
            foreach (Vector3 direction in directions)
            {
                currentPosition = VirtuallyChangeCurrentPosition(currentPosition, direction);
                if (currentPosition != oldCurrentPosition) break;
            }
            if (currentPosition == oldCurrentPosition)
            {
                currentPosition = Rollback(tempPath);
            }
            counter = (map[currentPosition] == 0) ? counter + 1 : 0;
        }
    }

    protected virtual List<Vector3> FindPathFromEnd(Vector3 end, Dictionary<Vector3, int> map)
    {
        Vector3 currentPosition = end;
        Vector3 start = character.gameObject.transform.position;
        List<Vector3> path = new List<Vector3>();
        path.Add(currentPosition);
        while (currentPosition != start)
        {
            currentPosition = StepBack(map, currentPosition);
            if (currentPosition == new Vector3(-1, -1, -1)) return null;
            path.Add(currentPosition);
        }
        return path;
    }

    protected virtual Vector3 StepBack(Dictionary<Vector3, int> map, Vector3 currentPosition)
    {
        if (map[currentPosition + Vector3.forward] == map[currentPosition] - 1)
        {
            return currentPosition + Vector3.forward;
        }
        else if (map[currentPosition + Vector3.back] == map[currentPosition] - 1)
        {
            return currentPosition + Vector3.back;
        }
        else if (map[currentPosition + Vector3.left] == map[currentPosition] - 1)
        {
            return currentPosition + Vector3.left;
        }
        else if (map[currentPosition + Vector3.right] == map[currentPosition] - 1)
        {
            return currentPosition + Vector3.right;
        }
        return new Vector3(-1, -1, -1);
    }

    public virtual Vector3 Rollback(List<Vector3> path)
    {
        if (path.Count == 0) return new Vector3(-1, -1, -1);
        if (path.Count != 1) path.RemoveAt(path.Count - 1);
        return path[path.Count - 1];
    }

    public virtual Vector3 VirtuallyChangeCurrentPosition(Vector3 currentPosition, Vector3 direction)
    {
        Dictionary<Vector3, int> map = character.fieldMap;
        if (map[currentPosition + direction] != -1 && map[currentPosition] + 1 < map[currentPosition + direction])
        {
            map[currentPosition + direction] = map[currentPosition] + 1;
            currentPosition = currentPosition + direction;
            tempPath.Add(currentPosition);
            character.fieldMap = map;
            return currentPosition;
        }
        return currentPosition;
    }

}

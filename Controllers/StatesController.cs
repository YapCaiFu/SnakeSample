using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SnakeSample.Models;

namespace SnakeSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatesController : ControllerBase
    {
        private readonly StateContext _context;

        public StatesController(StateContext context)
        {
            _context = context;
        }

        private async Task<State> addNew(int w, int h)
        {

            State state = new State( w, h);
            _context.States.Add(state);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw;
            }
            return state;
        }

        [HttpGet("new")]
        public async Task<ActionResult<State>> Get(int w,int h)
        {
            if(w <= 1 || h <= 1)
            {
                return BadRequest();
            }

            return new JsonResult(await addNew(w, h));
        }

        // GET: api/States
        [HttpGet]
        public async Task<ActionResult<IEnumerable<State>>> GetStates()
        {
          if (_context.States == null)
          {
              return NotFound();
          }
            return await _context.States.ToListAsync();
        }

        [HttpPost("validate")]
        public async Task<ActionResult<State>> Validate(ValidateData data)
        {
            var state = await _context.States.FindAsync(data.state.GameID);
            if (state == null || data.state.Fruit.X !=state.Fruit.X || data.state.Fruit.Y != state.Fruit.Y)
            {
                return NotFound("Invalid Game");
            }
            if (data == null || data.state.Snake.X != 0 || data.state.Snake.Y != 0 ||
                data.state.Snake.VelX!=1 || data.state.Snake.VelY!=0 ||
                data.state.Width !=state.Width || data.state.Height!=state.Height)
            {
                return BadRequest("Invalid Request");
            }
            int[] validMovment = new int[3] { 0, 1, -1 };
            state.Snake.X += state.Snake.VelX;
            state.Snake.Y += state.Snake.VelY;
            foreach (var tick in data.ticks)
            {
                if (tick.VelY == 1 && tick.VelX !=0 ||
                    tick.VelY == -1 && tick.VelX !=0 ||
                    tick.VelX == 1 && tick.VelY !=0 ||
                    tick.VelX == -1 && tick.VelY !=0 ||
                    !validMovment.Contains(tick.VelX) ||
                    !validMovment.Contains(tick.VelY)
                    )
                {
                    return BadRequest("Invalid Movement");
                }
                if (state.Snake.X == 1 && tick.VelX == -1 ||
                    state.Snake.X == -1 && tick.VelX == 1 ||
                    state.Snake.Y == 1 && tick.VelY == -1 ||
                    state.Snake.Y == -1 && tick.VelY == 1
                )
                {
                    return BadRequest("Invalid Movement Position");
                }
                state.Snake.VelX = tick.VelX;
                state.Snake.VelY = tick.VelY;

                state.Snake.X += state.Snake.VelX;
                state.Snake.Y += state.Snake.VelY;

                if (state.Snake.X>=state.Width || state.Snake.Y>=state.Height ||
                    state.Snake.X < 0 || state.Snake.Y < 0)
                {
                    State newState = await addNew(state.Width, state.Height);
                    _context.States.Remove(state);
                    await _context.SaveChangesAsync();
                    return StatusCode(StatusCodes.Status418ImATeapot, new { message = "Game is Over. New game created.", data = newState });
                }

                if (state.Snake.X == state.Fruit.X && state.Snake.Y == state.Fruit.Y)
                {
                    state.Score += 1;
                    state.Fruit = new Fruit(state.Width, state.Height);
                    state.Snake = new Snake();

                    try
                    {
                        _context.Entry(state).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateException)
                    {
                        throw;
                    }
                    return state;
                }
            }

            state.Fruit = new Fruit(state.Width, state.Height);
            state.Snake = new Snake();

            try
            {
                _context.Entry(state).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw;
            }
            return NotFound("The ticks do not lead the snake to the fruit position");
        }

    }
}
